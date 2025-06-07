USE Development;
GO

--Table creation
CREATE TABLE Stock (
    symbol VARCHAR(5) PRIMARY KEY,
    name VARCHAR(100)
);

CREATE TABLE StockPrice (
    symbol VARCHAR(5) PRIMARY KEY,
    price DECIMAL(18, 2),
    FOREIGN KEY (symbol) REFERENCES Stock(symbol)
);

CREATE TABLE StockBroker (
    id INT IDENTITY(1,1) PRIMARY KEY,  
    name VARCHAR(100),
    email VARCHAR(100),
    phone VARCHAR(20)
);

CREATE TABLE StockTransaction (
    id INT IDENTITY(1,1) PRIMARY KEY,                
    stocksymbol VARCHAR(5),                         
    quantity INT,
    unitprice DECIMAL(18, 2),
    brokerid INT,                                    
    FOREIGN KEY (stocksymbol) REFERENCES Stock(symbol),
    FOREIGN KEY (brokerid) REFERENCES StockBroker(id)
);

--Insertion

INSERT INTO Stock VALUES ('AAPL', 'Apple');
INSERT INTO Stock VALUES ('GOOGL', 'Alphabet');
INSERT INTO Stock VALUES ('MSFT', 'Microsoft');
INSERT INTO Stock VALUES ('AMZN', 'Amazon');
INSERT INTO Stock VALUES ('TSLA', 'Tesla');
INSERT INTO Stock VALUES ('META', 'Meta Platforms');
INSERT INTO Stock VALUES ('NFLX', 'Netflix');
INSERT INTO Stock VALUES ('NVDA', 'NVIDIA');
INSERT INTO Stock VALUES ('BABA', 'Alibaba');
INSERT INTO Stock VALUES ('INTC', 'Intel');
INSERT INTO Stock VALUES ('ORCL', 'Oracle');
INSERT INTO Stock VALUES ('IBM', 'IBM');
INSERT INTO Stock VALUES ('ADBE', 'Adobe');
INSERT INTO Stock VALUES ('CRM', 'Salesforce');
INSERT INTO Stock VALUES ('PYPL', 'PayPal');
INSERT INTO Stock VALUES ('AMD', 'Advanced Micro Devices');
INSERT INTO Stock VALUES ('UBER', 'Uber Technologies');
INSERT INTO Stock VALUES ('LYFT', 'Lyft');
INSERT INTO Stock VALUES ('SHOP', 'Shopify');
INSERT INTO Stock VALUES ('SQ', 'Block Inc.');

insert into StockPrice values ('AAPL',203.95);
INSERT INTO StockPrice VALUES ('GOOGL', 2784.32);
INSERT INTO StockPrice VALUES ('MSFT', 326.45);
INSERT INTO StockPrice VALUES ('AMZN', 134.21);
INSERT INTO StockPrice VALUES ('TSLA', 240.65);
INSERT INTO StockPrice VALUES ('META', 321.88);
INSERT INTO StockPrice VALUES ('NFLX', 445.02);
INSERT INTO StockPrice VALUES ('NVDA', 1056.78);
INSERT INTO StockPrice VALUES ('BABA', 88.64);
INSERT INTO StockPrice VALUES ('INTC', 42.10);
INSERT INTO StockPrice VALUES ('ORCL', 116.50);
INSERT INTO StockPrice VALUES ('IBM', 158.37);
INSERT INTO StockPrice VALUES ('ADBE', 523.99);
INSERT INTO StockPrice VALUES ('CRM', 273.60);
INSERT INTO StockPrice VALUES ('PYPL', 68.45);