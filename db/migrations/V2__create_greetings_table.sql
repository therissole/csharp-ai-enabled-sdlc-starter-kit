-- Create greetings table with UUID primary key and foreign key to languages
CREATE TABLE IF NOT EXISTS greetings (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    language_id UUID NOT NULL,
    greeting_text VARCHAR(255) NOT NULL,
    formal BOOLEAN NOT NULL DEFAULT false,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_language
        FOREIGN KEY(language_id)
        REFERENCES languages(id)
        ON DELETE CASCADE
);

-- Create index on language_id for faster lookups
CREATE INDEX idx_greetings_language_id ON greetings(language_id);

-- Insert some default greetings
INSERT INTO greetings (language_id, greeting_text, formal)
SELECT id, 'Hello', false FROM languages WHERE code = 'en'
UNION ALL
SELECT id, 'Good morning', true FROM languages WHERE code = 'en'
UNION ALL
SELECT id, 'Hola', false FROM languages WHERE code = 'es'
UNION ALL
SELECT id, 'Buenos días', true FROM languages WHERE code = 'es'
UNION ALL
SELECT id, '안녕하세요', false FROM languages WHERE code = 'ko'
UNION ALL
SELECT id, '좋은 아침입니다', true FROM languages WHERE code = 'ko'
ON CONFLICT DO NOTHING;
