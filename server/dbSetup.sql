CREATE TABLE IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL primary key COMMENT 'primary key', createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created', updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update', name varchar(255) COMMENT 'User Name', email varchar(255) COMMENT 'User Email', picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS listings (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT, createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created', updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update', name CHAR(255) NOT NULL, description VARCHAR(2000) NOT NULL, price INT NOT NULL DEFAULT 0, img VARCHAR(1000) NOT NULL, category CHAR(255) NOT NULL, quantity INT NOT NULL DEFAULT 0, creatorId VARCHAR(255) NOT NULL, FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS purchases (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT, listingId INT NOT NULL, creatorId VARCHAR(255) NOT NULL, FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE, FOREIGN KEY (listingId) REFERENCES listings (id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS receipts (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT, createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created', buyer VARCHAR(100) NOT NULL, street VARCHAR(100) NOT NULL, city VARCHAR(100) NOT NULL, state1 VARCHAR(100) NOT NULL, zip VARCHAR(100) NOT NULL, total INT NOT NULL DEFAULT 0, creatorId VARCHAR(255) NOT NULL, FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

SELECT * FROM listings

SELECT * FROM purchases

SELECT * FROM receipts

DROP TABLE purchases

DROP TABLE receipts

ALTER TABLE purchases
ADD COLUMN purchaseQuantity INT NOT NULL DEFAULT 0;

SELECT pur.*, lis.*, acc.*
FROM
    purchases pur
    JOIN listings lis ON lis.id = pur.listingId
    JOIN accounts acc ON acc.id = pur.creatorId
WHERE
    pur.creatorId = "6579f2b038eec22c9d9a6b0f";

SELECT lis.* FROM listings lis WHERE quantity > 0

INSERT INTO
    receipts (
        buyer, street, city, state1, zip, total, creatorId
    )
VALUES (
        @Buyer, @Street, @City, @State1, @Zip, @Total, @CreatorId
    );

SELECT rec.*, acc.*
FROM receipts rec
    JOIN accounts acc ON rec.creatorId = acc.id
WHERE
    rec.id = LAST_INSERT_ID();