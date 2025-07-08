-- Create the database
CREATE DATABASE EventTicketingSystem;

CREATE TABLE Tbl_Admin (
    UserId VARCHAR,
    UserCode VARCHAR,
    Username VARCHAR,
    Email VARCHAR,
    PhoneNo VARCHAR,
    Password VARCHAR,
    CreatedBy VARCHAR,
    CreatedAt TIMESTAMP,
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP,
    DeleteFlag BOOLEAN
);

CREATE TABLE Tbl_Category (
    CategoryId VARCHAR,
    CategoryCode VARCHAR,
    CategoryName VARCHAR,
    CreatedBy VARCHAR,
    CreatedAt TIMESTAMP,
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP,
    DeleteFlag BOOLEAN
);

CREATE TABLE Tbl_BusinessOwner (
    BusinessOwnerId VARCHAR,
    BusinessOwnerCode VARCHAR,
    Name VARCHAR,
    Email VARCHAR,
    PhoneNumber VARCHAR,
    CreatedBy VARCHAR,
    CreatedAt TIMESTAMP,
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP,
    DeleteFlag BOOLEAN
);

CREATE TABLE Tbl_BusinessEmail (
    BusinessEmailId VARCHAR,
    BusinessEmailCode VARCHAR,
    FullName VARCHAR,
    Phone VARCHAR,
    Email VARCHAR,
    CreatedBy VARCHAR,
    CreatedAt TIMESTAMP,
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP,
    DeleteFlag BOOLEAN
);

CREATE TABLE Tbl_Ticket (
    TicketId VARCHAR,
    TicketCode VARCHAR,
    TicketPriceCode VARCHAR,
    IsUsed BOOLEAN,
    CreatedBy VARCHAR,
    CreatedAt TIMESTAMP,
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP,
    DeleteFlag BOOLEAN
);

CREATE TABLE Tbl_TicketPrice (
    TicketPriceId VARCHAR,
    TicketPriceCode VARCHAR,
    EventCode VARCHAR,
    TicketTypeCode VARCHAR,
    TicketPrice DECIMAL(20,2),
    TicketQuantity INT,
    CreatedBy VARCHAR,
    CreatedAt TIMESTAMP,
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP,
    DeleteFlag BOOLEAN
);

CREATE TABLE Tbl_TicketType (
    TicketTypeId VARCHAR,
    TicketTypeCode VARCHAR,
    TicketTypeName VARCHAR,
    CreatedBy VARCHAR,
    CreatedAt TIMESTAMP,
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP,
    DeleteFlag BOOLEAN
);

CREATE TABLE Tbl_Event (
    EventId VARCHAR,
    EventCode VARCHAR,
    EventName VARCHAR,
    CategoryCode VARCHAR,
    Description VARCHAR,
    Address VARCHAR,
    StartDate TIMESTAMP,
    EndDate TIMESTAMP,
    EventImage VARCHAR,
    IsActive BOOLEAN,
    EventStatus VARCHAR,
    BusinessOwnerCode VARCHAR,
    TotalTicketQuantity INT,
    CreatedBy VARCHAR,
    CreatedAt TIMESTAMP,
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP,
    DeleteFlag BOOLEAN
);

CREATE TABLE Tbl_Venue (
    VenueId VARCHAR,
    VenueCode VARCHAR,
    VenueName VARCHAR,
    VenueDescription VARCHAR,
    VenueAddress VARCHAR,
    VenueCapacity INT,
    CreatedBy VARCHAR,
    CreatedAt TIMESTAMP,
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP,
    DeleteFlag BOOLEAN
);

CREATE TABLE Tbl_Transaction (
    TransactionId VARCHAR,
    TransactionCode VARCHAR,
    Email VARCHAR,
    EventCode VARCHAR,
    Status VARCHAR,
    PaymentType VARCHAR,
    TransactionDate TIMESTAMP,
    TotalAmount DECIMAL(20,2),
    CreatedBy VARCHAR,
    CreatedAt TIMESTAMP,
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP,
    DeleteFlag BOOLEAN
);

CREATE TABLE Tbl_TransactionTicket (
    TransactionTicketId VARCHAR,
    TransactionCode VARCHAR,
    TicketCode VARCHAR,
    QrString VARCHAR,
    Price DECIMAL(20,2),
    CreatedBy VARCHAR,
    CreatedAt TIMESTAMP,
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP,
    DeleteFlag BOOLEAN
);

CREATE TABLE Tbl_Verification (
    VerificationId VARCHAR,
    VerificationCode VARCHAR,
    Email VARCHAR,
    CreatedBy VARCHAR,
    CreatedAt TIMESTAMP,
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP,
    DeleteFlag BOOLEAN
);

CREATE TABLE Tbl_BusinessEmail (
    BusinessEmailId VARCHAR,
    BusinessEmailCode VARCHAR,
    FullName VARCHAR,
    Phone VARCHAR,
    Email VARCHAR,
    CreatedBy VARCHAR,
    CreatedAt TIMESTAMP,
    ModifiedBy VARCHAR,
    ModifiedAt TIMESTAMP,
    DeleteFlag BOOLEAN
);
