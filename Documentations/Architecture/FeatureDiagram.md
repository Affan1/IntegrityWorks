````mermaid
graph TB
    subgraph USER_SERVICE["👤 USER SERVICE"]
        US_AUTH["Authentication & Authorization"]
        US_PROFILE["Profile Management"]
        US_PREF["User Preferences"]
        US_ROLE["Role Management"]

        US_AUTH --> US_LOGIN["Login/Logout"]
        US_AUTH --> US_2FA["Two-Factor Authentication"]
        US_AUTH --> US_JWT["JWT Token Management"]
        US_AUTH --> US_OAUTH["OAuth Integration"]

        US_PROFILE --> US_BIO["Biography & Skills"]
        US_PROFILE --> US_PORTFOLIO["Portfolio/Gallery"]
        US_PROFILE --> US_RATING["Rating & Reviews"]
        US_PROFILE --> US_BADGES["Badges & Achievements"]

        US_PREF --> US_NOTIF_PREF["Notification Preferences"]
        US_PREF --> US_PRIVACY["Privacy Settings"]
        US_PREF --> US_LANG["Language & Locale"]

        US_ROLE --> US_CLIENT["Client Role"]
        US_ROLE --> US_FREELANCER["Freelancer Role"]
        US_ROLE --> US_ADMIN["Admin Role"]
    end

    subgraph JOB_SERVICE["💼 JOB SERVICE"]
        JS_CRUD["Job CRUD Operations"]
        JS_SEARCH["Search & Discovery"]
        JS_MATCH["Job Matching"]
        JS_STATUS["Status Management"]

        JS_CRUD --> JS_CREATE["Create Job Posting"]
        JS_CRUD --> JS_EDIT["Edit/Update Job"]
        JS_CRUD --> JS_DELETE["Delete/Archive Job"]
        JS_CRUD --> JS_TEMPLATE["Job Templates"]

        JS_SEARCH --> JS_FILTER["Advanced Filtering"]
        JS_SEARCH --> JS_FULLTEXT["Full-Text Search"]
        JS_SEARCH --> JS_GEO["Geographic Search"]
        JS_SEARCH --> JS_CATEGORY["Category Browsing"]

        JS_MATCH --> JS_SKILL["Skill Matching"]
        JS_MATCH --> JS_BUDGET["Budget Matching"]
        JS_MATCH --> JS_RECOMMEND["AI Recommendations"]

        JS_STATUS --> JS_DRAFT["Draft"]
        JS_STATUS --> JS_ACTIVE["Active"]
        JS_STATUS --> JS_FILLED["Filled"]
        JS_STATUS --> JS_EXPIRED["Expired"]
    end

    subgraph PROPOSAL_SERVICE["📝 PROPOSAL SERVICE"]
        PS_SUBMIT["Proposal Submission"]
        PS_REVIEW["Proposal Review"]
        PS_NEGOTIATE["Negotiation"]
        PS_ACCEPT["Acceptance Flow"]

        PS_SUBMIT --> PS_DRAFT["Save Draft"]
        PS_SUBMIT --> PS_VALIDATE["Validation Rules"]
        PS_SUBMIT --> PS_ATTACH["Attachments"]
        PS_SUBMIT --> PS_MILESTONE["Milestone Definition"]

        PS_REVIEW --> PS_COMPARE["Compare Proposals"]
        PS_REVIEW --> PS_SHORTLIST["Shortlisting"]
        PS_REVIEW --> PS_SCORE["Auto-Scoring"]

        PS_NEGOTIATE --> PS_COUNTER["Counter Offers"]
        PS_NEGOTIATE --> PS_MESSAGE["In-Proposal Messaging"]
        PS_NEGOTIATE --> PS_REVISION["Revision History"]

        PS_ACCEPT --> PS_CONTRACT["Contract Generation"]
        PS_ACCEPT --> PS_TERMS["Terms Acceptance"]
        PS_ACCEPT --> PS_ESCROW_INIT["Escrow Initialization"]
    end

    subgraph PAYMENT_SERVICE["💳 PAYMENT SERVICE"]
        PAY_PROCESS["Payment Processing"]
        PAY_METHOD["Payment Methods"]
        PAY_WITHDRAW["Withdrawals"]
        PAY_REFUND["Refunds & Disputes"]

        PAY_PROCESS --> PAY_CHARGE["Charge Processing"]
        PAY_PROCESS --> PAY_SPLIT["Payment Splitting"]
        PAY_PROCESS --> PAY_FEE["Fee Calculation"]
        PAY_PROCESS --> PAY_CURRENCY["Multi-Currency"]

        PAY_METHOD --> PAY_CARD["Credit/Debit Cards"]
        PAY_METHOD --> PAY_BANK["Bank Transfer"]
        PAY_METHOD --> PAY_WALLET["Digital Wallets"]
        PAY_METHOD --> PAY_CRYPTO["Cryptocurrency"]

        PAY_WITHDRAW --> PAY_SCHEDULE["Payout Schedule"]
        PAY_WITHDRAW --> PAY_THRESHOLD["Minimum Threshold"]
        PAY_WITHDRAW --> PAY_VERIFY["Withdrawal Verification"]

        PAY_REFUND --> PAY_DISPUTE["Dispute Resolution"]
        PAY_REFUND --> PAY_PARTIAL["Partial Refunds"]
        PAY_REFUND --> PAY_CHARGEBACK["Chargeback Handling"]
    end

    subgraph ESCROW_SERVICE["🔒 ESCROW SERVICE"]
        ESC_HOLD["Fund Holding"]
        ESC_MILESTONE["Milestone Management"]
        ESC_RELEASE["Fund Release"]
        ESC_DISPUTE["Dispute Escrow"]

        ESC_HOLD --> ESC_LOCK["Lock Funds"]
        ESC_HOLD --> ESC_INTEREST["Interest Calculation"]
        ESC_HOLD --> ESC_SECURITY["Security Measures"]

        ESC_MILESTONE --> ESC_DEFINE["Define Milestones"]
        ESC_MILESTONE --> ESC_APPROVE["Approval Workflow"]
        ESC_MILESTONE --> ESC_PARTIAL_REL["Partial Release"]

        ESC_RELEASE --> ESC_AUTO["Auto Release Timer"]
        ESC_RELEASE --> ESC_MANUAL["Manual Release"]
        ESC_RELEASE --> ESC_CONFIRM["Confirmation Required"]

        ESC_DISPUTE --> ESC_FREEZE["Freeze Funds"]
        ESC_DISPUTE --> ESC_ARBITRATION["Arbitration Process"]
        ESC_DISPUTE --> ESC_SPLIT_FUND["Split Fund Decision"]
    end

    subgraph ANTISCAM_SERVICE["🛡️ ANTI-SCAM SERVICE"]
        AS_DETECT["Detection Engine"]
        AS_ML["ML Models"]
        AS_PATTERN["Pattern Analysis"]
        AS_SCORE["Risk Scoring"]

        AS_DETECT --> AS_REALTIME["Real-Time Scanning"]
        AS_DETECT --> AS_BATCH["Batch Analysis"]
        AS_DETECT --> AS_CONTENT["Content Filtering"]
        AS_DETECT --> AS_BEHAVIOR["Behavioral Analysis"]

        AS_ML --> AS_CLASSIFY["Classification Models"]
        AS_ML --> AS_ANOMALY["Anomaly Detection"]
        AS_ML --> AS_NLP["NLP Text Analysis"]
        AS_ML --> AS_TRAIN["Model Training Pipeline"]

        AS_PATTERN --> AS_KNOWN["Known Scam Patterns"]
        AS_PATTERN --> AS_LINK["Suspicious Links"]
        AS_PATTERN --> AS_DUPLICATE["Duplicate Detection"]
        AS_PATTERN --> AS_VELOCITY["Velocity Checks"]

        AS_SCORE --> AS_USER_RISK["User Risk Score"]
        AS_SCORE --> AS_JOB_RISK["Job Risk Score"]
        AS_SCORE --> AS_PROPOSAL_RISK["Proposal Risk Score"]
        AS_SCORE --> AS_THRESHOLD["Threshold Management"]
    end

    subgraph STRIKE_SERVICE["⚠️ STRIKE SERVICE"]
        ST_ISSUE["Strike Issuance"]
        ST_TRACK["Strike Tracking"]
        ST_APPEAL["Appeal System"]
        ST_PENALTY["Penalty Enforcement"]

        ST_ISSUE --> ST_AUTO["Auto-Strike"]
        ST_ISSUE --> ST_MANUAL["Manual Strike"]
        ST_ISSUE --> ST_SEVERITY["Severity Levels"]
        ST_ISSUE --> ST_EVIDENCE["Evidence Collection"]

        ST_TRACK --> ST_HISTORY["Strike History"]
        ST_TRACK --> ST_COUNT["Strike Counter"]
        ST_TRACK --> ST_EXPIRY["Strike Expiration"]

        ST_APPEAL --> ST_SUBMIT_APPEAL["Submit Appeal"]
        ST_APPEAL --> ST_REVIEW_APPEAL["Review Process"]
        ST_APPEAL --> ST_OVERTURN["Overturn Decision"]
        ST_APPEAL --> ST_REJECT_APPEAL["Reject Appeal"]

        ST_PENALTY --> ST_WARN["Warning"]
        ST_PENALTY --> ST_RESTRICT["Feature Restriction"]
        ST_PENALTY --> ST_SUSPEND["Account Suspension"]
        ST_PENALTY --> ST_BAN["Permanent Ban"]
    end

    subgraph VERIFY_SERVICE["✅ VERIFICATION SERVICE"]
        VER_ID["Identity Verification"]
        VER_DOC["Document Verification"]
        VER_SKILL["Skill Verification"]
        VER_BUSINESS["Business Verification"]

        VER_ID --> VER_EMAIL["Email Verification"]
        VER_ID --> VER_PHONE["Phone Verification"]
        VER_ID --> VER_GOV_ID["Government ID"]
        VER_ID --> VER_FACE["Facial Recognition"]

        VER_DOC --> VER_UPLOAD["Document Upload"]
        VER_DOC --> VER_OCR["OCR Processing"]
        VER_DOC --> VER_VALIDATE["Validation Rules"]
        VER_DOC --> VER_EXPIRE["Expiration Tracking"]

        VER_SKILL --> VER_TEST["Skill Tests"]
        VER_SKILL --> VER_CERT["Certifications"]
        VER_SKILL --> VER_PORTFOLIO_CHECK["Portfolio Review"]

        VER_BUSINESS --> VER_TAX["Tax ID Verification"]
        VER_BUSINESS --> VER_LICENSE["Business License"]
        VER_BUSINESS --> VER_ADDRESS["Address Verification"]
    end

    subgraph ANALYTICS_SERVICE["📊 ANALYTICS SERVICE"]
        AN_METRICS["Platform Metrics"]
        AN_TRUST["Trust Score Engine"]
        AN_REPORT["Reporting"]
        AN_INSIGHT["Business Insights"]

        AN_METRICS --> AN_USER_METRICS["User Metrics"]
        AN_METRICS --> AN_JOB_METRICS["Job Metrics"]
        AN_METRICS --> AN_PAYMENT_METRICS["Payment Metrics"]
        AN_METRICS --> AN_PERFORMANCE["Performance KPIs"]

        AN_TRUST --> AN_CALC["Score Calculation"]
        AN_TRUST --> AN_FACTOR["Trust Factors"]
        AN_TRUST --> AN_HISTORY["Score History"]
        AN_TRUST --> AN_BADGE_AWARD["Badge Awards"]

        AN_REPORT --> AN_DASH["Dashboard"]
        AN_REPORT --> AN_EXPORT["Data Export"]
        AN_REPORT --> AN_SCHEDULE["Scheduled Reports"]
        AN_REPORT --> AN_CUSTOM["Custom Reports"]

        AN_INSIGHT --> AN_TREND["Trend Analysis"]
        AN_INSIGHT --> AN_PREDICT["Predictive Analytics"]
        AN_INSIGHT --> AN_SEGMENT["User Segmentation"]
        AN_INSIGHT --> AN_COHORT["Cohort Analysis"]
    end

    subgraph NOTIFY_SERVICE["🔔 NOTIFICATION SERVICE"]
        NOT_CHANNEL["Multi-Channel Delivery"]
        NOT_TEMPLATE["Template Management"]
        NOT_SCHEDULE["Scheduling"]
        NOT_PREF["Preference Management"]

        NOT_CHANNEL --> NOT_EMAIL["Email"]
        NOT_CHANNEL --> NOT_SMS["SMS"]
        NOT_CHANNEL --> NOT_PUSH["Push Notifications"]
        NOT_CHANNEL --> NOT_INAPP["In-App Notifications"]

        NOT_TEMPLATE --> NOT_CREATE["Template Creation"]
        NOT_TEMPLATE --> NOT_VARIABLE["Variable Substitution"]
        NOT_TEMPLATE --> NOT_LOCALE_TEMP["Localization"]
        NOT_TEMPLATE --> NOT_VERSION["Version Control"]

        NOT_SCHEDULE --> NOT_IMMEDIATE["Immediate Delivery"]
        NOT_SCHEDULE --> NOT_BATCH_NOT["Batch Delivery"]
        NOT_SCHEDULE --> NOT_DIGEST["Digest Mode"]
        NOT_SCHEDULE --> NOT_RETRY["Retry Logic"]

        NOT_PREF --> NOT_FREQUENCY["Frequency Control"]
        NOT_PREF --> NOT_QUIET["Quiet Hours"]
        NOT_PREF --> NOT_UNSUBSCRIBE["Unsubscribe Management"]
    end

    style USER_SERVICE fill:#e3f2fd
    style JOB_SERVICE fill:#f3e5f5
    style PROPOSAL_SERVICE fill:#fff3e0
    style PAYMENT_SERVICE fill:#e8f5e9
    style ESCROW_SERVICE fill:#fce4ec
    style ANTISCAM_SERVICE fill:#ffebee
    style STRIKE_SERVICE fill:#fff9c4
    style VERIFY_SERVICE fill:#e0f2f1
    style ANALYTICS_SERVICE fill:#f1f8e9
    style NOTIFY_SERVICE fill:#ede7f6
    ```
````
