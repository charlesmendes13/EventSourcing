CREATE TABLE Events (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AggregateId UNIQUEIDENTIFIER NOT NULL,
    EventType NVARCHAR(MAX) NOT NULL,
    EventData NVARCHAR(MAX) NOT NULL,
    Timestamp DATETIME NOT NULL
);