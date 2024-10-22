CREATE TABLE IF NOT EXISTS Wallets(
  id varchar(32) not null primary key,
  name varchar(100) not null,
  taxId varchar(14) not null,
  accountBranch varchar(2) not null,
  accountNumber varchar(6) not null,
  balance  decimal(20, 2) null,
  createdAt TIMESTAMP not null
);

CREATE TABLE IF NOT EXISTS Transactions(
  id varchar(32) not null primary key,
  payerId varchar(32) not null,
  receiverId varchar(32) not null,
  amount  decimal(20, 2) null,
  transactionDate TIMESTAMP not null,
  createdAt TIMESTAMP not null,
  FOREIGN KEY (payerId) REFERENCES Wallets(id),
  FOREIGN KEY (receiverId) REFERENCES Wallets(id)
);
