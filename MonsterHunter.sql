CREATE DATABASE MonsterHunter;


-- Creating Tables
-- 3 tables total: Player, Weapon, WeaponType
-- 3 primary keys, 2 foreign keys, 2 additional constraints
CREATE TABLE WeaponTypes (
    WeaponTypeID INT NOT NULL AUTO_INCREMENT,
    WeaponTypeName VARCHAR(16) NOT NULL UNIQUE,

    PRIMARY KEY (WeaponTypeID)
);


CREATE TABLE Weapons (
    WeaponID INT NOT NULL AUTO_INCREMENT,
    WeaponTypeID INT,
    WeaponName VARCHAR(22) NOT NULL UNIQUE,
    ATK INT NOT NULL,
    Critical INT,


    PRIMARY KEY (WeaponID),
    FOREIGN KEY (WeaponTypeID) REFERENCES WeaponTypes(WeaponTypeID),
    CHECK (ATK > 0), -- additional constraint
    CHECK (Critical >= -50 and Critical <= 100)
);


CREATE TABLE Players (
    PlayerID INT NOT NULL AUTO_INCREMENT,
    PlayerName VARCHAR(16) NOT NULL,
    WeaponID INT,

    PRIMARY KEY (PlayerID),
    FOREIGN KEY (WeaponID) REFERENCES Weapons(WeaponID)
);





-- Inserting data into tables
-- WeponTypes table should be complete
-- since there's no PUT/POST/DELETE for this table
INSERT INTO WeaponTypes (WeaponTypeName) VALUES 
("Great Sword"), ("Long Sword"), ("Sword and Shield"),
("Dual Blades"), ("Lance"), ("Gunlance"), ("Hammer"),
("Hunting Horn"), ("Switch Axe"), ("Charge Blade"),
("Insect Glaive"), ("Light Bowgun"), ("Heavy Bowgun"),
("Bow");

-- add a few weapons to Weapons table
INSERT INTO Weapons (WeaponTypeID, WeaponName, ATK, Critical) VALUES
(1, "Kamura Cleaver I", 50, 0),
(2, "Kamura Blade I", 50, 0),
(3, "Kamura Sword I", 50, 0),
(4, "Kamura Glintblades I", 50, 0),
(5, "Kamura Spear I", 50, 0),
(6, "Kamura Gunlance I", 50, 0),
(7, "Kamura Hammer I", 50, 0),
(8, "Kamura Chorus I", 50, 0),
(9, "Kamura Iron Axe I", 50, 0),
(10, "Kamura C. Blade I", 50, 0),
(11, "Kamura Glaive I", 50, 0),
(12, "Kamura L. Bowgun I", 50, 0),
(13, "Kamura H. Bowgun I", 60, 0),
(14, "Kamura Iron Bow I", 60, 0);

-- add a player to Players table
INSERT INTO Players (PlayerName, WeaponID) VALUES
("MonsterHunter", 4),
("RayRay", 1),
("ProMH", 2),
("Daniel321", 10),
("JustinBeBird", 6),
("iamhungry", 14);


