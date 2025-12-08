```mermaid
graph TB
    subgraph SEQ1["📋 SEQUENCE 1: Job Creation with Fraud Detection"]
        direction LR
        S1_NOTE["Sequence Diagram:<br/>Client → Gateway → Job Service → Anti-Scam<br/><br/>Flow:<br/>1. Client submits job<br/>2. Job Service validates<br/>3. gRPC call to Anti-Scam<br/>4. Check Redis cache for user risk<br/>5. Query user history from DB<br/>6. Run ML model inference<br/>7. Pattern matching against known scams<br/>8. Calculate risk score 0-100<br/><br/>Outcomes:<br/>• Risk > 80: Auto-reject + alert admins<br/>• Risk 50-80: Manual review required<br/>• Risk < 50: Auto-approve + notify freelancers<br/><br/>Events Published:<br/>- FraudDetected → Analytics + Notification<br/>- JobPendingReview → Notification<br/>- JobCreated → Analytics + Notification"]
    end

    subgraph SEQ2["💰 SEQUENCE 2: Proposal Acceptance to Escrow Lock"]
        direction LR
        S2_NOTE["Sequence Diagram:<br/>Client → Gateway → Proposal Service → Payment → Escrow<br/><br/>Flow:<br/>1. Client accepts proposal<br/>2. Proposal Service updates status = ACCEPTED<br/>3. Generate contract terms<br/>4. Publish ProposalAccepted event<br/>5. Payment Service calculates total + 15% fee<br/>6. Process payment via Stripe/PayPal<br/>7. Store transaction in SQL Server<br/><br/>Payment Failed Path:<br/>- Update status = FAILED<br/>- Publish PaymentFailed event<br/>- Notify client to retry<br/><br/>Payment Success Path:<br/>8. gRPC call to Escrow Service<br/>9. Create escrow account<br/>10. Create milestone escrows<br/>11. Set auto_release_at +14 days<br/>12. Publish FundsLocked event<br/>13. Notify both parties work can begin"]
    end

    subgraph SEQ3["⚠️ SEQUENCE 3: Strike Issuance and Appeal"]
        direction LR
        S3_NOTE["Sequence Diagram:<br/>Anti-Scam → RabbitMQ → Strike Service → User Service<br/><br/>Detection & Strike Flow:<br/>1. Anti-Scam detects fraud pattern<br/>2. Publish FraudDetected event<br/>3. Strike Service queries user history<br/>4. Determine severity based on:<br/>   - Fraud type<br/>   - User history<br/>   - Impact level<br/>5. Insert strike + evidence to DB<br/>6. Calculate penalty points<br/>7. Update user_penalty_status<br/><br/>Progressive Penalties:<br/>• 1st Strike: WARNING - Email only<br/>• 2nd Strike: RESTRICTION - 7 days<br/>• 3rd Strike: SUSPENSION - 30 days<br/>• 4th Strike: PERMANENT BAN<br/><br/>Appeal Process:<br/>8. User submits appeal with reason<br/>9. Publish AppealSubmitted event<br/>10. Admin reviews evidence<br/>11. Decision: Approve or Reject<br/>12. If approved: Restore account via gRPC<br/>13. If rejected: Penalty stands"]
    end

    subgraph SEQ4["🔧 SEQUENCE 4: Milestone Completion & Dispute"]
        direction LR
        S4_NOTE["Sequence Diagram:<br/>Freelancer → Job Service → Escrow → Payment<br/><br/>Completion Flow:<br/>1. Freelancer submits milestone completion<br/>2. Publish MilestoneCompleted event<br/>3. Notify client for approval<br/><br/>Client Approves:<br/>4. Escrow releases milestone funds<br/>5. gRPC call to Payment Service<br/>6. Process payout to freelancer via Stripe<br/>7. Update milestone status = RELEASED<br/>8. Publish MilestoneReleased event<br/>9. Update analytics & trust score<br/><br/>Client Disputes:<br/>4. Create dispute record<br/>5. Freeze ALL milestone funds<br/>6. Update status = DISPUTED<br/>7. Notify both parties + admins<br/>8. Evidence submission phase<br/>9. Message exchange between parties<br/><br/>Admin Arbitration:<br/>10. Review all evidence<br/>11. Make split decision e.g. 70% freelancer, 30% client<br/>12. Process split payment<br/>13. Update dispute status = RESOLVED<br/><br/>Auto-Release:<br/>If 14 days pass with no action:<br/>- Cron job auto-approves milestone<br/>- Transfer funds to freelancer<br/>- Notify both parties"]
    end

    subgraph STATE1["🔄 STATE: Job Lifecycle"]
        direction TB
        ST1_START((" "))
        ST1_DRAFT["Draft<br/>───<br/>Being created<br/>Not visible"]
        ST1_PENDING["PendingReview<br/>───<br/>Medium Risk<br/>Manual review"]
        ST1_ACTIVE["Active<br/>───<br/>Low Risk<br/>Accepting proposals"]
        ST1_REJECTED["Rejected<br/>───<br/>High Risk or<br/>Admin rejected"]
        ST1_FILLED["Filled<br/>───<br/>Proposal accepted<br/>No more bids"]
        ST1_INPROG["InProgress<br/>───<br/>Work ongoing<br/>Milestones tracked"]
        ST1_REVIEW["UnderReview<br/>───<br/>Work submitted<br/>Awaiting approval"]
        ST1_DISPUTED["Disputed<br/>───<br/>Funds frozen<br/>Admin arbitration"]
        ST1_COMPLETED["Completed<br/>───<br/>Payment released<br/>Reviews submitted"]
        ST1_CANCELLED["Cancelled<br/>───<br/>Client cancelled"]
        ST1_EXPIRED["Expired<br/>───<br/>Deadline passed"]
        ST1_END((" "))

        ST1_START --> ST1_DRAFT
        ST1_DRAFT --> ST1_PENDING
        ST1_DRAFT --> ST1_ACTIVE
        ST1_DRAFT --> ST1_REJECTED
        ST1_DRAFT --> ST1_CANCELLED
        ST1_PENDING --> ST1_ACTIVE
        ST1_PENDING --> ST1_REJECTED
        ST1_ACTIVE --> ST1_FILLED
        ST1_ACTIVE --> ST1_EXPIRED
        ST1_ACTIVE --> ST1_CANCELLED
        ST1_FILLED --> ST1_INPROG
        ST1_INPROG --> ST1_REVIEW
        ST1_INPROG --> ST1_DISPUTED
        ST1_REVIEW --> ST1_COMPLETED
        ST1_REVIEW --> ST1_DISPUTED
        ST1_DISPUTED --> ST1_COMPLETED
        ST1_DISPUTED --> ST1_CANCELLED
        ST1_COMPLETED --> ST1_END
        ST1_REJECTED --> ST1_END
        ST1_CANCELLED --> ST1_END
        ST1_EXPIRED --> ST1_END
    end

    subgraph STATE2["📝 STATE: Proposal Lifecycle"]
        direction TB
        ST2_START((" "))
        ST2_DRAFT["Draft<br/>───<br/>Being created<br/>Can edit freely"]
        ST2_SUBMITTED["Submitted<br/>───<br/>Sent to client<br/>No more edits"]
        ST2_REVIEW["UnderReview<br/>───<br/>Client viewing<br/>Evaluating"]
        ST2_SHORT["Shortlisted<br/>───<br/>In final selection<br/>High chance"]
        ST2_NEGOTIATE["Negotiating<br/>───<br/>Counter offers<br/>Terms revision"]
        ST2_ACCEPTED["Accepted<br/>───<br/>Client chose<br/>Payment initiated"]
        ST2_CONTRACT["ContractSigned<br/>───<br/>Both signed<br/>Work begins"]
        ST2_REJECTED["Rejected<br/>───<br/>Client declined"]
        ST2_WITHDRAWN["Withdrawn<br/>───<br/>Freelancer cancelled"]
        ST2_EXPIRED["Expired<br/>───<br/>Time limit exceeded"]
        ST2_END((" "))

        ST2_START --> ST2_DRAFT
        ST2_DRAFT --> ST2_SUBMITTED
        ST2_DRAFT --> ST2_WITHDRAWN
        ST2_SUBMITTED --> ST2_REVIEW
        ST2_SUBMITTED --> ST2_WITHDRAWN
        ST2_SUBMITTED --> ST2_EXPIRED
        ST2_REVIEW --> ST2_SHORT
        ST2_REVIEW --> ST2_REJECTED
        ST2_REVIEW --> ST2_NEGOTIATE
        ST2_SHORT --> ST2_NEGOTIATE
        ST2_SHORT --> ST2_ACCEPTED
        ST2_SHORT --> ST2_REJECTED
        ST2_NEGOTIATE --> ST2_REVIEW
        ST2_NEGOTIATE --> ST2_WITHDRAWN
        ST2_ACCEPTED --> ST2_CONTRACT
        ST2_CONTRACT --> ST2_END
        ST2_REJECTED --> ST2_END
        ST2_WITHDRAWN --> ST2_END
        ST2_EXPIRED --> ST2_END
    end

    subgraph STATE3["💳 STATE: Payment Transaction Lifecycle"]
        direction TB
        ST3_START((" "))
        ST3_PENDING["Pending<br/>───<br/>Payment initiated<br/>Validating"]
        ST3_PROCESSING["Processing<br/>───<br/>Gateway called<br/>Stripe/PayPal"]
        ST3_ACTION["RequiresAction<br/>───<br/>3DS required<br/>User must confirm"]
        ST3_COMPLETED["Completed<br/>───<br/>Charge successful<br/>Funds secured"]
        ST3_PARTIAL["PartiallyRefunded<br/>───<br/>Partial refund<br/>Some returned"]
        ST3_REFUNDED["Refunded<br/>───<br/>Full refund<br/>All returned"]
        ST3_DISPUTED["Disputed<br/>───<br/>Chargeback filed<br/>Evidence phase"]
        ST3_WON["DisputeWon<br/>───<br/>Evidence accepted<br/>Funds retained"]
        ST3_LOST["DisputeLost<br/>───<br/>Chargeback upheld<br/>Funds returned"]
        ST3_FAILED["Failed<br/>───<br/>Charge declined<br/>Insufficient funds"]
        ST3_EXPIRED["Expired<br/>───<br/>Timeout 30min<br/>User didn't confirm"]
        ST3_END((" "))

        ST3_START --> ST3_PENDING
        ST3_PENDING --> ST3_PROCESSING
        ST3_PENDING --> ST3_FAILED
        ST3_PROCESSING --> ST3_COMPLETED
        ST3_PROCESSING --> ST3_FAILED
        ST3_PROCESSING --> ST3_ACTION
        ST3_ACTION --> ST3_PROCESSING
        ST3_ACTION --> ST3_FAILED
        ST3_ACTION --> ST3_EXPIRED
        ST3_COMPLETED --> ST3_PARTIAL
        ST3_COMPLETED --> ST3_REFUNDED
        ST3_COMPLETED --> ST3_DISPUTED
        ST3_PARTIAL --> ST3_REFUNDED
        ST3_DISPUTED --> ST3_WON
        ST3_DISPUTED --> ST3_LOST
        ST3_WON --> ST3_COMPLETED
        ST3_LOST --> ST3_REFUNDED
        ST3_COMPLETED --> ST3_END
        ST3_REFUNDED --> ST3_END
        ST3_FAILED --> ST3_END
        ST3_EXPIRED --> ST3_END
    end

    subgraph STATE4["🔒 STATE: Escrow Account Lifecycle"]
        direction TB
        ST4_START((" "))
        ST4_INIT["Initialized<br/>───<br/>Payment received<br/>Account created"]
        ST4_FUNDED["Funded<br/>───<br/>Funds deposited<br/>Milestones ready"]
        ST4_M1["Milestone 1<br/>───<br/>$500 locked<br/>Auto-release 14d"]
        ST4_M2["Milestone 2<br/>───<br/>$750 locked<br/>Auto-release 14d"]
        ST4_M3["Milestone 3<br/>───<br/>$1000 locked<br/>Auto-release 14d"]
        ST4_PARTIAL["PartiallyReleased<br/>───<br/>Some milestones paid<br/>Others pending"]
        ST4_FULL["FullyReleased<br/>───<br/>All milestones paid<br/>Job complete"]
        ST4_DISPUTED["Disputed<br/>───<br/>Issue raised<br/>All funds frozen"]
        ST4_FROZEN["Frozen<br/>───<br/>Admin investigating<br/>No movement"]
        ST4_RESOLVED["Resolved<br/>───<br/>Decision made<br/>Processing split"]
        ST4_CLOSED["Closed<br/>───<br/>Transfer complete<br/>Account archived"]
        ST4_REFUNDED["Refunded<br/>───<br/>Returned to client"]
        ST4_CANCELLED["Cancelled<br/>───<br/>Job cancelled<br/>Funds returned"]
        ST4_END((" "))

        ST4_START --> ST4_INIT
        ST4_INIT --> ST4_FUNDED
        ST4_FUNDED --> ST4_M1
        ST4_M1 --> ST4_M2
        ST4_M2 --> ST4_M3
        ST4_M3 --> ST4_PARTIAL
        ST4_FUNDED --> ST4_PARTIAL
        ST4_FUNDED --> ST4_DISPUTED
        ST4_FUNDED --> ST4_CANCELLED
        ST4_PARTIAL --> ST4_PARTIAL
        ST4_PARTIAL --> ST4_FULL
        ST4_PARTIAL --> ST4_DISPUTED
        ST4_DISPUTED --> ST4_FROZEN
        ST4_FROZEN --> ST4_RESOLVED
        ST4_RESOLVED --> ST4_PARTIAL
        ST4_RESOLVED --> ST4_FULL
        ST4_RESOLVED --> ST4_REFUNDED
        ST4_FULL --> ST4_CLOSED
        ST4_REFUNDED --> ST4_CLOSED
        ST4_CANCELLED --> ST4_REFUNDED
        ST4_CLOSED --> ST4_END
    end

    subgraph STATE5["⚠️ STATE: Strike Lifecycle"]
        direction TB
        ST5_START((" "))
        ST5_ISSUED["Issued<br/>───<br/>Strike assigned<br/>Grace period"]
        ST5_ACTIVE["Active<br/>───<br/>Penalty active<br/>Restrictions applied"]
        ST5_WARN["Warning<br/>───<br/>1st Strike<br/>Email only"]
        ST5_RESTRICT["Restriction<br/>───<br/>2nd Strike<br/>7 days limited"]
        ST5_SUSPEND["Suspension<br/>───<br/>3rd Strike<br/>30 days banned"]
        ST5_BAN["Permanent Ban<br/>───<br/>4th Strike<br/>Account deleted"]
        ST5_APPEALED["Appealed<br/>───<br/>User contested<br/>Under review"]
        ST5_REVIEWING["UnderReview<br/>───<br/>Admin reviewing<br/>Evidence analysis"]
        ST5_UPHELD["Upheld<br/>───<br/>Appeal rejected<br/>Penalty stands"]
        ST5_OVERTURNED["Overturned<br/>───<br/>Appeal approved<br/>Strike removed"]
        ST5_EXPIRED["Expired<br/>───<br/>Duration completed<br/>Penalty lifted"]
        ST5_REMOVED["Removed<br/>───<br/>Strike deleted<br/>Record cleared"]
        ST5_END((" "))

        ST5_START --> ST5_ISSUED
        ST5_ISSUED --> ST5_ACTIVE
        ST5_ISSUED --> ST5_APPEALED
        ST5_ACTIVE --> ST5_WARN
        ST5_ACTIVE --> ST5_RESTRICT
        ST5_ACTIVE --> ST5_SUSPEND
        ST5_ACTIVE --> ST5_BAN
        ST5_ACTIVE --> ST5_EXPIRED
        ST5_ACTIVE --> ST5_APPEALED
        ST5_WARN --> ST5_RESTRICT
        ST5_RESTRICT --> ST5_SUSPEND
        ST5_SUSPEND --> ST5_BAN
        ST5_APPEALED --> ST5_REVIEWING
        ST5_REVIEWING --> ST5_UPHELD
        ST5_REVIEWING --> ST5_OVERTURNED
        ST5_UPHELD --> ST5_ACTIVE
        ST5_UPHELD --> ST5_EXPIRED
        ST5_OVERTURNED --> ST5_REMOVED
        ST5_BAN --> ST5_END
        ST5_EXPIRED --> ST5_END
        ST5_REMOVED --> ST5_END
    end

    subgraph STATE6["✅ STATE: Verification Lifecycle"]
        direction TB
        ST6_START((" "))
        ST6_NOT["NotVerified<br/>───<br/>Account created<br/>Trust: 0-20"]
        ST6_EMAIL_P["EmailPending<br/>───<br/>OTP sent<br/>Awaiting confirm"]
        ST6_PHONE_P["PhonePending<br/>───<br/>SMS sent<br/>Awaiting confirm"]
        ST6_DOC_P["DocumentPending<br/>───<br/>ID uploaded<br/>Processing"]
        ST6_EMAIL_V["EmailVerified<br/>───<br/>Email confirmed<br/>Trust: +10"]
        ST6_PHONE_V["PhoneVerified<br/>───<br/>Phone confirmed<br/>Trust: +10"]
        ST6_DOC_R["DocumentReview<br/>───<br/>Auto-check running<br/>OCR + Face match"]
        ST6_MANUAL["ManualReview<br/>───<br/>Flagged suspicious<br/>Admin checking"]
        ST6_DOC_V["DocumentVerified<br/>───<br/>ID approved<br/>Trust: +10"]
        ST6_DOC_REJ["DocumentRejected<br/>───<br/>ID failed<br/>Can re-upload"]
        ST6_FULLY["FullyVerified<br/>───<br/>All verified<br/>Trust: +30 total"]
        ST6_END((" "))

        ST6_START --> ST6_NOT
        ST6_NOT --> ST6_EMAIL_P
        ST6_NOT --> ST6_PHONE_P
        ST6_NOT --> ST6_DOC_P
        ST6_EMAIL_P --> ST6_EMAIL_V
        ST6_EMAIL_P --> ST6_NOT
        ST6_PHONE_P --> ST6_PHONE_V
        ST6_PHONE_P --> ST6_NOT
        ST6_DOC_P --> ST6_DOC_R
        ST6_DOC_P --> ST6_DOC_REJ
        ST6_DOC_P --> ST6_MANUAL
        ST6_DOC_R --> ST6_DOC_V
        ST6_DOC_R --> ST6_MANUAL
        ST6_MANUAL --> ST6_DOC_V
        ST6_MANUAL --> ST6_DOC_REJ
        ST6_DOC_REJ --> ST6_DOC_P
        ST6_EMAIL_V --> ST6_FULLY
        ST6_PHONE_V --> ST6_FULLY
        ST6_DOC_V --> ST6_FULLY
        ST6_FULLY --> ST6_END
    end

    subgraph STATE7["📊 STATE: Trust Score Calculation"]
        direction TB
        ST7_START((" "))
        ST7_CALC["Calculating<br/>───<br/>Event received<br/>Gathering factors"]
        ST7_VER["Verification<br/>───<br/>Email +10<br/>Phone +10<br/>ID +10"]
        ST7_COMP["Completion<br/>───<br/>Job rate<br/>0-30 points"]
        ST7_REV["Reviews<br/>───<br/>Avg rating<br/>0-25 points"]
        ST7_PAY["Payment<br/>───<br/>History clean<br/>0-20 points"]
        ST7_TEN["Tenure<br/>───<br/>Account age<br/>0-15 points"]
        ST7_STRIKE["Strike Penalty<br/>───<br/>Violations<br/>-5 to -50"]
        ST7_TOTAL["Total Score<br/>───<br/>Sum all factors<br/>Range: 0-100"]
        ST7_UPDATED["Updated<br/>───<br/>Score computed<br/>Store in DB"]
        ST7_RISING["Rising<br/>───<br/>Score increased<br/>Positive trend"]
        ST7_FALLING["Falling<br/>───<br/>Score decreased<br/>Warning sign"]
        ST7_STABLE["Stable<br/>───<br/>No change<br/>Maintaining level"]
        ST7_BADGE_AWARD["BadgeAwarded<br/>───<br/>Threshold crossed<br/>Notify user"]
        ST7_BADGE_REVOKE["BadgeRevoked<br/>───<br/>Below threshold<br/>Notify user"]
        ST7_FLAGGED["FlaggedForReview<br/>───<br/>Critical drop<br/>Alert admin"]
        ST7_END((" "))

        ST7_START --> ST7_CALC
        ST7_CALC --> ST7_VER
        ST7_CALC --> ST7_COMP
        ST7_CALC --> ST7_REV
        ST7_CALC --> ST7_PAY
        ST7_CALC --> ST7_TEN
        ST7_CALC --> ST7_STRIKE
        ST7_VER --> ST7_TOTAL
        ST7_COMP --> ST7_TOTAL
        ST7_REV --> ST7_TOTAL
        ST7_PAY --> ST7_TOTAL
        ST7_TEN --> ST7_TOTAL
        ST7_STRIKE --> ST7_TOTAL
        ST7_TOTAL --> ST7_UPDATED
        ST7_UPDATED --> ST7_RISING
        ST7_UPDATED --> ST7_FALLING
        ST7_UPDATED --> ST7_STABLE
        ST7_RISING --> ST7_BADGE_AWARD
        ST7_RISING --> ST7_END
        ST7_FALLING --> ST7_BADGE_REVOKE
        ST7_FALLING --> ST7_FLAGGED
        ST7_FALLING --> ST7_END
        ST7_STABLE --> ST7_END
        ST7_BADGE_AWARD --> ST7_END
        ST7_BADGE_REVOKE --> ST7_END
        ST7_FLAGGED --> ST7_END
    end

    style SEQ1 fill:#e3f2fd
    style SEQ2 fill:#e8f5e9
    style SEQ3 fill:#ffebee
    style SEQ4 fill:#fff3e0
    style STATE1 fill:#f3e5f5
    style STATE2 fill:#fff9c4
    style STATE3 fill:#e0f2f1
    style STATE4 fill:#fce4ec
    style STATE5 fill:#ffe0b2
    style STATE6 fill:#f1f8e9
    style STATE7 fill:#e1bee7
```
