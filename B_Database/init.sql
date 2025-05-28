CREATE TABLE IF NOT EXISTS User (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Email TEXT NOT NULL UNIQUE,
    Password TEXT NOT NULL
);

INSERT INTO User (Email, Password) VALUES ('initial.user@example.com', 'InitialP@ss1');

CREATE TABLE IF NOT EXISTS OlympicWinners (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    athlete TEXT,
    age INTEGER,
    country TEXT,
    year INTEGER,
    date TEXT,
    sport TEXT,
    gold INTEGER,
    silver INTEGER,
    bronze INTEGER,
    total INTEGER
);
