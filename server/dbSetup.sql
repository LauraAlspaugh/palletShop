CREATE TABLE IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL primary key COMMENT 'primary key', createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created', updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update', name varchar(255) COMMENT 'User Name', email varchar(255) COMMENT 'User Email', picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS listings (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT, createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created', updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update', name CHAR(255) NOT NULL, description VARCHAR(2000) NOT NULL, price INT NOT NULL DEFAULT 0, img VARCHAR(1000) NOT NULL, category CHAR(255) NOT NULL, quantity INT NOT NULL DEFAULT 0, creatorId VARCHAR(255) NOT NULL, FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS purchases (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT, listingId INT NOT NULL, creatorId VARCHAR(255) NOT NULL, FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE, FOREIGN KEY (listingId) REFERENCES listings (id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

SELECT * FROM listings

SELECT * FROM purchases

DROP TABLE purchases

ALTER TABLE purchases
ADD COLUMN FOREIGN KEY (listingId) REFERENCES listings (id) ON DELETE CASCADE;

SELECT pur.*, lis.*, acc.*
FROM
    purchases pur
    JOIN listings lis ON lis.id = pur.listingId
    JOIN accounts acc ON acc.id = pur.creatorId
WHERE
    pur.creatorId = "6579f2b038eec22c9d9a6b0f";

SELECT lis.*
FROM listings lis
WHERE
    quantity > 0