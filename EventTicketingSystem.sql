INSERT INTO Tbl_Admin (
    UserId,
    UserCode,
    Username,
    Email,
    PhoneNo,
    Password,
    CreatedBy,
    CreatedAt,
    ModifiedBy,
    ModifiedAt,
    DeleteFlag
) VALUES (
    'U001',
    'ADM001',
    'admin_user',
    'admin@example.com',
    '09123456789',
    'securepassword123',
    'system',
    NOW(),
    'system',
    NOW(),
    FALSE
);

select * from tbl_Admin;
select * from tbl_businessowner;
select * from tbl_category;

INSERT INTO tbl_businessowner (
    Businessownerid,
    Businessownercode,
    Name,
    Email,
    Phonenumber,
    Createdby,
    Createdat,
    Modifiedby,
    Modifiedat,
    Deleteflag
) VALUES (
    'BO001',
    'BO-CODE-001',
    'Jane Doe',
    'janedoe@example.com',
    '09111222333',
    'system',
    NOW(),
    'system',
    NOW(),
    FALSE
);
