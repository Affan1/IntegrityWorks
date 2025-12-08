```mermaid
erDiagram
    %% ========================================
    %% USER SERVICE - PostgreSQL
    %% ========================================

    USERS ||--o{ USER_ROLES : has
    USERS ||--o{ USER_PROFILES : has
    USERS ||--o{ USER_PREFERENCES : has
    USERS ||--o{ USER_SESSIONS : has
    USERS ||--o{ USER_BADGES : earns
    USERS ||--o{ PASSWORD_RESET_TOKENS : requests
    USERS ||--o{ REFRESH_TOKENS : has

    USERS {
        uuid id PK
        string email UK
        string username UK
        string password_hash
        boolean email_verified
        boolean phone_verified
        datetime created_at
        datetime updated_at
        datetime last_login
        boolean is_active
        boolean is_deleted
        int trust_score
        string account_type
    }

    USER_PROFILES {
        uuid id PK
        uuid user_id FK
        string first_name
        string last_name
        string phone
        text bio
        string profile_picture_url
        string location
        string timezone
        json skills
        json portfolio_links
        decimal average_rating
        int total_reviews
        int jobs_completed
        datetime created_at
        datetime updated_at
    }

    USER_ROLES {
        uuid id PK
        uuid user_id FK
        string role_name
        datetime assigned_at
    }

    USER_PREFERENCES {
        uuid id PK
        uuid user_id FK
        boolean email_notifications
        boolean sms_notifications
        boolean push_notifications
        string notification_frequency
        string language
        string currency
        json privacy_settings
        datetime updated_at
    }

    USER_SESSIONS {
        uuid id PK
        uuid user_id FK
        string session_token UK
        string ip_address
        string user_agent
        datetime created_at
        datetime expires_at
    }

    USER_BADGES {
        uuid id PK
        uuid user_id FK
        string badge_type
        string badge_name
        datetime earned_at
    }

    PASSWORD_RESET_TOKENS {
        uuid id PK
        uuid user_id FK
        string token UK
        datetime created_at
        datetime expires_at
        boolean used
    }

    REFRESH_TOKENS {
        uuid id PK
        uuid user_id FK
        string token UK
        datetime created_at
        datetime expires_at
        boolean revoked
    }

    %% ========================================
    %% JOB SERVICE - PostgreSQL
    %% ========================================

    JOBS ||--o{ JOB_CATEGORIES : belongs_to
    JOBS ||--o{ JOB_SKILLS : requires
    JOBS ||--o{ JOB_ATTACHMENTS : has
    JOBS ||--o{ JOB_VIEWS : tracks
    JOBS ||--o{ JOB_BOOKMARKS : has
    JOBS ||--o{ JOB_STATUS_HISTORY : tracks

    JOBS {
        uuid id PK
        uuid client_id FK
        string title
        text description
        string status
        decimal budget_min
        decimal budget_max
        string budget_type
        string experience_level
        datetime deadline
        string location_type
        string location
        json custom_questions
        int proposal_count
        datetime created_at
        datetime updated_at
        datetime published_at
        datetime closed_at
        boolean is_featured
        decimal scam_risk_score
    }

    JOB_CATEGORIES {
        uuid id PK
        uuid job_id FK
        uuid category_id FK
        datetime assigned_at
    }

    CATEGORIES {
        uuid id PK
        string name UK
        string slug UK
        uuid parent_id FK
        text description
        boolean is_active
    }

    JOB_SKILLS {
        uuid id PK
        uuid job_id FK
        uuid skill_id FK
        string proficiency_level
        datetime assigned_at
    }

    SKILLS {
        uuid id PK
        string name UK
        string slug UK
        uuid category_id FK
        boolean is_active
    }

    JOB_ATTACHMENTS {
        uuid id PK
        uuid job_id FK
        string file_name
        string file_url
        string file_type
        bigint file_size
        datetime uploaded_at
    }

    JOB_VIEWS {
        uuid id PK
        uuid job_id FK
        uuid user_id FK
        datetime viewed_at
        string ip_address
    }

    JOB_BOOKMARKS {
        uuid id PK
        uuid job_id FK
        uuid user_id FK
        datetime bookmarked_at
    }

    JOB_STATUS_HISTORY {
        uuid id PK
        uuid job_id FK
        string old_status
        string new_status
        uuid changed_by FK
        text reason
        datetime changed_at
    }

    %% ========================================
    %% PROPOSAL SERVICE - PostgreSQL
    %% ========================================

    PROPOSALS ||--o{ PROPOSAL_MILESTONES : contains
    PROPOSALS ||--o{ PROPOSAL_ATTACHMENTS : has
    PROPOSALS ||--o{ PROPOSAL_MESSAGES : has
    PROPOSALS ||--o{ PROPOSAL_REVISIONS : tracks

    PROPOSALS {
        uuid id PK
        uuid job_id FK
        uuid freelancer_id FK
        string status
        decimal bid_amount
        string currency
        int delivery_days
        text cover_letter
        json custom_answers
        datetime submitted_at
        datetime accepted_at
        datetime rejected_at
        datetime withdrawn_at
        datetime created_at
        datetime updated_at
        decimal scam_risk_score
        boolean is_shortlisted
    }

    PROPOSAL_MILESTONES {
        uuid id PK
        uuid proposal_id FK
        string title
        text description
        decimal amount
        int delivery_days
        int order_number
        datetime created_at
    }

    PROPOSAL_ATTACHMENTS {
        uuid id PK
        uuid proposal_id FK
        string file_name
        string file_url
        string file_type
        bigint file_size
        datetime uploaded_at
    }

    PROPOSAL_MESSAGES {
        uuid id PK
        uuid proposal_id FK
        uuid sender_id FK
        text message
        json attachments
        boolean is_read
        datetime sent_at
    }

    PROPOSAL_REVISIONS {
        uuid id PK
        uuid proposal_id FK
        int revision_number
        decimal bid_amount
        int delivery_days
        text changes_summary
        datetime created_at
    }

    %% ========================================
    %% PAYMENT SERVICE - SQL Server
    %% ========================================

    PAYMENT_ACCOUNTS ||--o{ PAYMENT_METHODS : has
    PAYMENT_ACCOUNTS ||--o{ TRANSACTIONS : performs
    PAYMENT_ACCOUNTS ||--o{ PAYOUTS : receives
    TRANSACTIONS ||--o{ TRANSACTION_FEES : incurs
    TRANSACTIONS ||--o{ REFUNDS : has

    PAYMENT_ACCOUNTS {
        uniqueidentifier id PK
        uniqueidentifier user_id UK
        nvarchar stripe_customer_id
        nvarchar paypal_account_id
        decimal account_balance
        nvarchar currency
        bit is_verified
        datetime2 created_at
        datetime2 updated_at
    }

    PAYMENT_METHODS {
        uniqueidentifier id PK
        uniqueidentifier account_id FK
        nvarchar method_type
        nvarchar provider
        nvarchar external_id
        nvarchar last_four
        nvarchar card_brand
        int expiry_month
        int expiry_year
        bit is_default
        bit is_active
        datetime2 created_at
    }

    TRANSACTIONS {
        uniqueidentifier id PK
        uniqueidentifier account_id FK
        uniqueidentifier job_id FK
        uniqueidentifier proposal_id FK
        nvarchar transaction_type
        nvarchar status
        decimal amount
        nvarchar currency
        decimal platform_fee
        nvarchar payment_method_id
        nvarchar external_transaction_id
        nvarchar description
        datetime2 created_at
        datetime2 completed_at
        datetime2 failed_at
        nvarchar failure_reason
    }

    TRANSACTION_FEES {
        uniqueidentifier id PK
        uniqueidentifier transaction_id FK
        nvarchar fee_type
        decimal amount
        decimal percentage
        nvarchar description
        datetime2 calculated_at
    }

    REFUNDS {
        uniqueidentifier id PK
        uniqueidentifier transaction_id FK
        decimal amount
        nvarchar reason
        nvarchar status
        nvarchar external_refund_id
        datetime2 requested_at
        datetime2 processed_at
    }

    PAYOUTS {
        uniqueidentifier id PK
        uniqueidentifier account_id FK
        decimal amount
        nvarchar currency
        nvarchar status
        nvarchar payout_method
        nvarchar external_payout_id
        datetime2 requested_at
        datetime2 processed_at
        datetime2 failed_at
        nvarchar failure_reason
    }

    %% ========================================
    %% ESCROW SERVICE - SQL Server
    %% ========================================

    ESCROW_ACCOUNTS ||--o{ ESCROW_TRANSACTIONS : tracks
    ESCROW_ACCOUNTS ||--o{ MILESTONE_ESCROWS : contains
    ESCROW_ACCOUNTS ||--o{ ESCROW_DISPUTES : has
    ESCROW_DISPUTES ||--o{ DISPUTE_MESSAGES : contains
    ESCROW_DISPUTES ||--o{ DISPUTE_EVIDENCE : has

    ESCROW_ACCOUNTS {
        uniqueidentifier id PK
        uniqueidentifier job_id UK
        uniqueidentifier proposal_id UK
        uniqueidentifier client_id FK
        uniqueidentifier freelancer_id FK
        decimal total_amount
        decimal held_amount
        decimal released_amount
        nvarchar status
        datetime2 created_at
        datetime2 updated_at
    }

    MILESTONE_ESCROWS {
        uniqueidentifier id PK
        uniqueidentifier escrow_account_id FK
        int milestone_number
        nvarchar milestone_title
        decimal amount
        nvarchar status
        datetime2 funded_at
        datetime2 released_at
        datetime2 auto_release_at
    }

    ESCROW_TRANSACTIONS {
        uniqueidentifier id PK
        uniqueidentifier escrow_account_id FK
        uniqueidentifier milestone_id FK
        nvarchar transaction_type
        decimal amount
        nvarchar status
        nvarchar description
        datetime2 created_at
    }

    ESCROW_DISPUTES {
        uniqueidentifier id PK
        uniqueidentifier escrow_account_id FK
        uniqueidentifier raised_by FK
        nvarchar dispute_type
        nvarchar status
        nvarchar description
        decimal disputed_amount
        datetime2 created_at
        datetime2 resolved_at
        uniqueidentifier resolved_by FK
        nvarchar resolution
        decimal client_amount
        decimal freelancer_amount
    }

    DISPUTE_MESSAGES {
        uniqueidentifier id PK
        uniqueidentifier dispute_id FK
        uniqueidentifier sender_id FK
        nvarchar message
        bit is_admin_message
        datetime2 sent_at
    }

    DISPUTE_EVIDENCE {
        uniqueidentifier id PK
        uniqueidentifier dispute_id FK
        uniqueidentifier submitted_by FK
        nvarchar file_name
        nvarchar file_url
        nvarchar file_type
        nvarchar description
        datetime2 uploaded_at
    }

    %% ========================================
    %% ANTI-SCAM SERVICE - PostgreSQL + Redis
    %% ========================================

    SCAM_REPORTS ||--o{ SCAM_PATTERNS : matches
    SCAM_REPORTS ||--o{ SCAM_EVIDENCE : has
    USERS_RISK_PROFILE ||--o{ RISK_FACTORS : contains
    ML_PREDICTIONS ||--o{ PREDICTION_FEATURES : uses

    SCAM_REPORTS {
        uuid id PK
        uuid reported_entity_id FK
        string entity_type
        uuid reported_by FK
        string report_type
        text description
        string status
        decimal risk_score
        datetime reported_at
        datetime reviewed_at
        uuid reviewed_by FK
        string resolution
    }

    SCAM_PATTERNS {
        uuid id PK
        uuid report_id FK
        string pattern_type
        string pattern_name
        json pattern_data
        decimal confidence_score
        datetime detected_at
    }

    SCAM_EVIDENCE {
        uuid id PK
        uuid report_id FK
        string evidence_type
        text evidence_data
        string file_url
        datetime collected_at
    }

    USERS_RISK_PROFILE {
        uuid id PK
        uuid user_id UK
        decimal overall_risk_score
        string risk_level
        int suspicious_actions_count
        datetime last_calculated
        datetime created_at
        datetime updated_at
    }

    RISK_FACTORS {
        uuid id PK
        uuid risk_profile_id FK
        string factor_type
        string factor_name
        decimal weight
        decimal score
        json metadata
        datetime recorded_at
    }

    ML_PREDICTIONS {
        uuid id PK
        uuid entity_id FK
        string entity_type
        string model_name
        string model_version
        decimal prediction_score
        string prediction_class
        json raw_output
        datetime predicted_at
    }

    PREDICTION_FEATURES {
        uuid id PK
        uuid prediction_id FK
        string feature_name
        string feature_value
        decimal importance
    }

    KNOWN_SCAM_PATTERNS {
        uuid id PK
        string pattern_name UK
        string pattern_type
        json pattern_rules
        decimal severity
        boolean is_active
        datetime created_at
        datetime updated_at
    }

    SUSPICIOUS_LINKS {
        uuid id PK
        string url UK
        string domain
        string threat_type
        decimal threat_score
        datetime detected_at
        datetime last_seen
        int occurrence_count
    }

    %% ========================================
    %% STRIKE SERVICE - PostgreSQL
    %% ========================================

    STRIKES ||--o{ STRIKE_APPEALS : has
    STRIKES ||--o{ STRIKE_EVIDENCE : includes
    PENALTY_RULES ||--o{ STRIKES : applies_to

    STRIKES {
        uuid id PK
        uuid user_id FK
        string violation_type
        string severity
        text description
        decimal penalty_points
        string status
        uuid issued_by FK
        datetime issued_at
        datetime expires_at
        boolean is_active
        json metadata
    }

    STRIKE_APPEALS {
        uuid id PK
        uuid strike_id FK
        uuid appealed_by FK
        text appeal_reason
        string status
        datetime submitted_at
        datetime reviewed_at
        uuid reviewed_by FK
        text review_notes
        string decision
    }

    STRIKE_EVIDENCE {
        uuid id PK
        uuid strike_id FK
        string evidence_type
        text evidence_description
        string file_url
        json evidence_data
        datetime collected_at
    }

    PENALTY_RULES {
        uuid id PK
        string violation_type UK
        string severity
        decimal penalty_points
        int duration_days
        json restrictions
        string action
        boolean is_active
        datetime created_at
    }

    USER_PENALTY_STATUS {
        uuid id PK
        uuid user_id UK
        int total_strikes
        decimal total_penalty_points
        string current_status
        json active_restrictions
        datetime restriction_ends_at
        datetime last_updated
    }

    %% ========================================
    %% VERIFICATION SERVICE - SQLite
    %% ========================================

    VERIFICATIONS ||--o{ VERIFICATION_DOCUMENTS : includes
    VERIFICATIONS ||--o{ VERIFICATION_CHECKS : performs

    VERIFICATIONS {
        text id PK
        text user_id UK
        text verification_type
        text status
        text verification_level
        text created_at
        text completed_at
        text expires_at
        integer is_expired
        text metadata
    }

    VERIFICATION_DOCUMENTS {
        text id PK
        text verification_id FK
        text document_type
        text file_name
        text file_url
        text file_hash
        text status
        text uploaded_at
        text verified_at
        text ocr_data
    }

    VERIFICATION_CHECKS {
        text id PK
        text verification_id FK
        text check_type
        text status
        text result
        real confidence_score
        text performed_at
        text metadata
    }

    VERIFICATION_ATTEMPTS {
        text id PK
        text user_id FK
        text verification_type
        text status
        text failure_reason
        text attempted_at
    }

    %% ========================================
    %% ANALYTICS SERVICE - TimescaleDB
    %% ========================================

    USER_METRICS ||--o{ USER_ACTIVITY_EVENTS : aggregates
    JOB_METRICS ||--o{ JOB_EVENTS : aggregates
    PAYMENT_METRICS ||--o{ PAYMENT_EVENTS : aggregates
    TRUST_SCORES ||--o{ TRUST_SCORE_HISTORY : tracks

    USER_METRICS {
        timestamptz time PK
        uuid user_id PK
        bigint active_days
        bigint login_count
        bigint jobs_posted
        bigint proposals_submitted
        bigint jobs_completed
        decimal total_earned
        decimal total_spent
        decimal avg_rating
        bigint total_reviews
    }

    USER_ACTIVITY_EVENTS {
        uuid id PK
        uuid user_id FK
        string event_type
        json event_data
        timestamptz occurred_at
    }

    JOB_METRICS {
        timestamptz time PK
        uuid category_id PK
        bigint total_jobs_posted
        bigint total_jobs_filled
        decimal avg_budget
        decimal avg_completion_time
        bigint total_proposals
        decimal avg_proposals_per_job
    }

    JOB_EVENTS {
        uuid id PK
        uuid job_id FK
        string event_type
        json event_data
        timestamptz occurred_at
    }

    PAYMENT_METRICS {
        timestamptz time PK
        string currency PK
        decimal total_volume
        bigint transaction_count
        bigint successful_transactions
        bigint failed_transactions
        decimal total_fees_collected
        bigint refund_count
        decimal refund_amount
    }

    PAYMENT_EVENTS {
        uuid id PK
        uuid transaction_id FK
        string event_type
        json event_data
        timestamptz occurred_at
    }

    TRUST_SCORES {
        uuid id PK
        uuid user_id UK
        decimal score
        string level
        json score_breakdown
        timestamptz calculated_at
        timestamptz updated_at
    }

    TRUST_SCORE_HISTORY {
        uuid id PK
        uuid user_id FK
        decimal old_score
        decimal new_score
        string change_reason
        json factors_changed
        timestamptz changed_at
    }

    PLATFORM_METRICS {
        timestamptz time PK
        bigint total_users
        bigint active_users
        bigint total_jobs
        bigint active_jobs
        decimal total_transaction_volume
        bigint total_disputes
        bigint total_strikes
    }
```
