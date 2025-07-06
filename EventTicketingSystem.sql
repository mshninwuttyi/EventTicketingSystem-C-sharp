-- Create the database
CREATE DATABASE EventTicketingSystem;

-- Connect to the database
\c EventTicketingSystem

-- Create Tbl_Admin table
CREATE TABLE Tbl_Admin (
    UserId VARCHAR PRIMARY KEY,
    UserCode VARCHAR NOT NULL,
    Username VARCHAR NOT NULL,
    Email VARCHAR NOT NULL,
    Password VARCHAR NOT NULL,
    CreateBy VARCHAR,
    CreateAt TIMESTAMP(2),
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP(2),
    DeleteFlag BOOLEAN DEFAULT FALSE
);

-- Create Tbl_Event table
CREATE TABLE Tbl_Event (
    EventId VARCHAR PRIMARY KEY,
    EventCode VARCHAR NOT NULL,
    EventName VARCHAR NOT NULL,
    CategoryCode VARCHAR,
    Description TEXT,
    Address TEXT,
    StartDate TIMESTAMP(2),
    EndDate TIMESTAMP(2),
    EventImage VARCHAR,
    IsActive BOOLEAN DEFAULT TRUE,
    EventStatus VARCHAR,
    BusinessOwnerCode VARCHAR,
    TotalTicketQuantity INTEGER,
    CreateBy VARCHAR,
    CreateAt TIMESTAMP(2),
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP(2),
    DeleteFlag BOOLEAN DEFAULT FALSE,
    VenueID VARCHAR,
    VenueCode VARCHAR,
    VenueName VARCHAR,
    VenueDescription TEXT,
    VenueAddress TEXT,
    VenueCapacity INTEGER,
    VenueImage VARCHAR
);

-- Create Tbl_TicketType table
CREATE TABLE Tbl_TicketType (
    TicketTypeId VARCHAR PRIMARY KEY,
    TicketTypeCode VARCHAR NOT NULL,
    TicketTypeName VARCHAR NOT NULL,
    CreateBy VARCHAR,
    CreateAt TIMESTAMP(2),
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP(2),
    DeleteFlag BOOLEAN DEFAULT FALSE
);

-- Create Tbl_TicketPrice table
CREATE TABLE Tbl_TicketPrice (
    TicketPriceId VARCHAR PRIMARY KEY,
    TicketPriceCode VARCHAR NOT NULL,
    EventCode VARCHAR NOT NULL,
    TicketTypeCode VARCHAR NOT NULL,
    TicketPrice DECIMAL(20, 2) NOT NULL,
    TicketQuantity INTEGER NOT NULL,
    CreateBy VARCHAR,
    CreateAt TIMESTAMP(2),
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP(2),
    DeleteFlag BOOLEAN DEFAULT FALSE
);

-- Create Tbl_Ticket table
CREATE TABLE Tbl_Ticket (
    TicketId VARCHAR PRIMARY KEY,
    TicketCode VARCHAR NOT NULL,
    TicketPriceCode VARCHAR NOT NULL,
    isUsed BOOLEAN DEFAULT FALSE,
    CreateBy VARCHAR,
    CreateAt TIMESTAMP(2),
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP(2),
    DeleteFlag BOOLEAN DEFAULT FALSE
);

-- Create Tbl_Transaction table
CREATE TABLE Tbl_Transaction (
    TransactionId VARCHAR PRIMARY KEY,
    TransactionCode VARCHAR NOT NULL,
    Email VARCHAR NOT NULL,
    EventCode VARCHAR NOT NULL,
    Status VARCHAR NOT NULL,
    PaymentType VARCHAR,
    TransactionDate TIMESTAMP(2) NOT NULL,
    TotalAmount DECIMAL(20, 2) NOT NULL,
    CreateBy VARCHAR,
    CreateAt TIMESTAMP(2),
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP(2),
    DeleteFlag BOOLEAN DEFAULT FALSE
);

-- Create TransactionTicket table
CREATE TABLE TransactionTicket (
    TransactionTicketId VARCHAR PRIMARY KEY,
    TransactionCode VARCHAR NOT NULL,
    TicketCode VARCHAR NOT NULL,
    QiString VARCHAR,
    Price DECIMAL(20, 2) NOT NULL,
    CreateBy VARCHAR,
    CreateAt TIMESTAMP(2),
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP(2),
    DeleteFlag BOOLEAN DEFAULT FALSE
);

-- Create Tbl_Verification table
CREATE TABLE Tbl_Verification (
    VerificationId VARCHAR PRIMARY KEY,
    VerificationCode VARCHAR NOT NULL,
    Email VARCHAR NOT NULL,
    CreatedBy VARCHAR,
    CreatedAt TIMESTAMP(2),
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP(2),
    DeleteFlag BOOLEAN DEFAULT FALSE,
    BusinessEmailId VARCHAR,
    BusinessEmailCode VARCHAR,
    FullName VARCHAR,
    Phone VARCHAR,
    CreateBy VARCHAR,
    CreateAt TIMESTAMP(2)
);

-- Create Tbl_Category table
CREATE TABLE Tbl_Category (
    CategoryId VARCHAR PRIMARY KEY,
    CategoryCode VARCHAR NOT NULL,
    CategoryName VARCHAR NOT NULL,
    CreateBy VARCHAR,
    CreateAt TIMESTAMP(2),
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP(2),
    DeleteFlag BOOLEAN DEFAULT FALSE
);

-- Create Tbl_BusinessOwner table
CREATE TABLE Tbl_BusinessOwner (
    BusinessOwnerId VARCHAR PRIMARY KEY,
    BusinessOwnerCode VARCHAR NOT NULL,
    Name VARCHAR NOT NULL,
    Email VARCHAR NOT NULL,
    PhoneNumber VARCHAR,
    CreateBy VARCHAR,
    CreateAt TIMESTAMP(2),
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP(2),
    DeleteFlag BOOLEAN DEFAULT FALSE
);