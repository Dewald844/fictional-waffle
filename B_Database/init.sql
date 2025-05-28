CREATE TABLE IF NOT EXISTS User (
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Email TEXT NOT NULL UNIQUE,
                                    Password TEXT NOT NULL
);

INSERT INTO User (Email, Password) VALUES ('initial.user@example.com', 'InitialP@ss1');
