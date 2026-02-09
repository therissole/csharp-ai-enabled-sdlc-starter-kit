-- Create languages table with UUID primary key
CREATE TABLE IF NOT EXISTS languages (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name VARCHAR(100) NOT NULL UNIQUE,
    code VARCHAR(10) NOT NULL UNIQUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Create index on language code for faster lookups
CREATE INDEX idx_languages_code ON languages(code);

-- Insert some default languages
INSERT INTO languages (name, code) VALUES
    ('English', 'en'),
    ('Spanish', 'es'),
    ('Korean', 'ko')
ON CONFLICT (code) DO NOTHING;
