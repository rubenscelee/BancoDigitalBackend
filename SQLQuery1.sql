use BancoDigitalApi

CREATE TABLE UserApi(
	 Id uniqueIdentifier default (newId()) primary key,
	 Email varchar(100) not null,
	 IsEnabled BIT,
	 CreatedOn DateTime not null,
);

ALTER TABLE UserApi
	ADD CONSTRAINT FK_User_BankAccount FOREIGN KEY (BankAccountId) REFERENCES BankAccount(Id)

CREATE TABLE BankAccount (
    Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    Agencia VARCHAR(5),
    Conta VARCHAR(10),
    Banco VARCHAR(15),
    CreatedOn DATETIME NOT NULL,
    UserId UNIQUEIDENTIFIER UNIQUE NOT NULL,
    CONSTRAINT FK_BankAccount_User FOREIGN KEY (UserId) REFERENCES UserApi(Id)
);

ALTER TABLE BankAccount
	ADD Saldo MONEY DEFAULT 0.00


CREATE TABLE BankTransferPix(
	Id uniqueIdentifier default (newId()) primary key,
    Title varchar(100) not null,
    DescriptionText varchar(500) not null,
	TransferAmount MONEY DEFAULT 0.00,
	BankAccountSenderId UNIQUEIDENTIFIER UNIQUE NOT NULL,
	BankAccountReceiverId UNIQUEIDENTIFIER UNIQUE NOT NULL,
	CreatedOn DateTime not null,
	ChavePix varchar(500),
	CONSTRAINT FK_BankAccount_BankTransferPix_Sender FOREIGN KEY (BankAccountSenderId) REFERENCES BankAccount(Id),
	CONSTRAINT FK_BankAccount_BankTransferPix_Receiver FOREIGN KEY (BankAccountReceiverId) REFERENCES BankAccount(Id)
);

CREATE TABLE Pix(
	Id uniqueIdentifier default (newId()) primary key,
    ChavePix varchar(100) not null,
	CreatedOn DateTime not null,
	BankAccountId UNIQUEIDENTIFIER UNIQUE NOT NULL,
	CONSTRAINT FK_BankAccount_Pix FOREIGN KEY (BankAccountId) REFERENCES BankAccount(Id)
);


select * from UserApi
select * from dbo.BankAccount
select * from dbo.Pix

DROP TABLE BankAccount;


delete from UserApi

INSERT INTO UserApi (Id, Email, IsEnabled, CreatedOn)
VALUES 
('11111111-1111-1111-1111-111111111111', 'user1@example.com', 1, GETUTCDATE()),
('11111111-1111-1111-1111-111111111112', 'user2@example.com', 1, GETUTCDATE());

INSERT INTO BankAccount (Id, Agencia, Conta, Banco, CreatedOn, UserId)
VALUES
('22222222-2222-2222-2222-222222222222', '0001', '123456', 'MyBank', GETUTCDATE(), '11111111-1111-1111-1111-111111111111'),
('22222222-2222-2222-2222-222222222223', '0002', '654321', 'AnotherBank', GETUTCDATE(), '11111111-1111-1111-1111-111111111112');

INSERT INTO Pix (Id, ChavePix, CreatedOn, BankAccountId)
VALUES
(NEWID(), 'user1@email.com',GETUTCDATE(), '22222222-2222-2222-2222-222222222222'),
(NEWID(), '12345678900', GETUTCDATE(), '22222222-2222-2222-2222-222222222223');



















































































































































