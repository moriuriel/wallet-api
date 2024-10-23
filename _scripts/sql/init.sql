CREATE TABLE IF NOT EXISTS wallets(
  id varchar(36) not null primary key,
  name varchar(100) not null,
  tax_id varchar(14) not null,
  account_branch varchar(2) not null,
  account_number varchar(6) not null,
  balance  decimal(20, 2) null,
  created_at TIMESTAMP not null
);

CREATE TABLE IF NOT EXISTS transactions(
  id varchar(36) not null primary key,
  payer_id varchar(36) not null,
  receiver_id varchar(36) not null,
  amount  decimal(20, 2) null,
  transaction_date TIMESTAMP not null,
  created_at TIMESTAMP not null,
  FOREIGN KEY (payer_id) REFERENCES Wallets(id),
  FOREIGN KEY (receiver_id) REFERENCES Wallets(id)
);
