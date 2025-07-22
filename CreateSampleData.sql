-- Sample ULIDs generated for illustration (you can generate your own using a ULID library or tool)
INSERT INTO Tbl_Admin (
    UserId, UserCode, Username, Email, Password, 
    CreatedBy, CreatedAt, ModifiedBy, ModifiedAt, DeleteFlag
) VALUES
(
    '01H9Z7F6X1QV3ZP6BK3G2M18ZJ', 'ADM001', 'alice_admin', 'alice@example.com', 'P@ssw0rd1',
    'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9Z7F9J9RKVBC3QW1ZQZXWME', 'ADM002', 'bob_admin', 'bob@example.com', 'P@ssw0rd2',
    'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9Z7FBTCA8B6F5EBDVKMKXEN', 'ADM003', 'charlie_admin', 'charlie@example.com', 'P@ssw0rd3',
    'system', CURRENT_TIMESTAMP, 'system', CURRENT_TIMESTAMP, TRUE
);

-- Sample data for Tbl_Category
INSERT INTO Tbl_Category (
    CategoryId, CategoryCode, CategoryName, 
    CreatedBy, CreatedAt, ModifiedBy, ModifiedAt, DeleteFlag
) VALUES
(
    '01H9Z8R5YQBYFJZVM2NHKTP84W', 'CAT001', 'Music Concert',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9Z8R8X7EKXEVJY7Y57PX9XJ', 'CAT002', 'Tech Conference',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9Z8RB5PNA8ACG0XZ5TZMRNP', 'CAT003', 'Art Exhibition',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9Z8RDB84YPW8N51GJ1F8BKS', 'CAT004', 'Charity Gala',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, TRUE
),
(
    '01H9Z8RFJ5VKFHQW8EZDTPDZWM', 'CAT005', 'Sports Tournament',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
);

-- Sample data for Tbl_VenueType

INSERT INTO Tbl_VenueType (
    VenueTypeId, VenueTypeCode, VenueTypeName, 
    CreatedBy, CreatedAt, ModifiedBy, ModifiedAt, DeleteFlag
) VALUES
(
    '01H9ZB1WQ9SYB3N4TFJ8RZNP9W', 'VEN001', 'Stadium',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZB1YP7KM0H6D2WG89RMDQG', 'VEN002', 'Convention Center',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZB21AXWYTZAKG72EZ0SX10', 'VEN003', 'Concert Hall',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZB2335MEECHEZYZ8NYP74R', 'VEN004', 'Outdoor Park',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZB24MVVY0AVMH8BEMH0J8X', 'VEN005', 'Theater',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZB26YV7YSDR6ZFP2NBMWM0', 'VEN006', 'Hotel Ballroom',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZB285NRRG2VQZJ9KEK1NXB', 'VEN007', 'Exhibition Hall',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZB2A4ZMA7N9W1EJHK2EZR2', 'VEN008', 'Beachfront',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZB2BZK1PA6TDR2CH5YGWZT', 'VEN009', 'University Auditorium',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZB2D9C5FCPMZ8K49X4TGNR', 'VEN010', 'Cultural Center',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, TRUE
);

-- Sample data for Tbl_Venue

INSERT INTO Tbl_Venue (
    VenueId, VenueCode, VenueName, VenueDetailCode, VenueTypeCode,
    VenueDescription, VenueAddress, VenueCapacity,
    VenueFacilities, VenueAddons,
    CreatedBy, CreatedAt, ModifiedBy, ModifiedAt, DeleteFlag
) VALUES
-- Stadiums
('01H9ZC1WYA1B6ZP3MBFGYKJV7G', 'VENU001', 'Grand Arena', 'VD001', 'VEN001',
 'Multipurpose stadium suitable for sports and concerts.', '99 Stadium Blvd, Metro City', 50000,
 'Parking, Restrooms, VIP Lounge', 'Fireworks Setup, Drone Show',
 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),

('01H9ZC1Y8ZXKXK7MZVXQ0N4YHH', 'VENU002', 'City Sports Dome', 'VD002', 'VEN001',
 'Enclosed stadium with retractable roof.', '22 Victory Lane, Capital Town', 35000,
 'Wi-Fi, Locker Rooms, Digital Scoreboards', 'Virtual Reality Booths',
 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),

-- Convention Centers
('01H9ZC21V9NW8WG0MTRBCJ6Y1B', 'VENU003', 'Global Convention Center', 'VD003', 'VEN002',
 'State-of-the-art center for trade shows and expos.', '101 Trade Rd, Business Park', 12000,
 'Projectors, High-speed Internet, Cafeteria', 'Language Translation Pods',
 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),

('01H9ZC23FZ6EZHNYW9YDC4B36N', 'VENU004', 'Riverfront Expo Hall', 'VD004', 'VEN002',
 'Scenic convention venue by the river.', '5 River St, Coastal Bay', 8000,
 'Open-air Decks, Charging Stations', 'Water Taxi Access',
 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),

-- Concert Halls
('01H9ZC25H8PYE3TEQ5NVH9YN4X', 'VENU005', 'Echo Sound Hall', 'VD005', 'VEN003',
 'Modern acoustics optimized for live performances.', '48 Soundwave Ave, Music City', 2500,
 'Green Rooms, Stage Lighting, Acoustic Treatment', 'AR-Enhanced Experience',
 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),

('01H9ZC270W7TXGPWJ46KVPW6VA', 'VENU006', 'Starline Auditorium', 'VD006', 'VEN003',
 'Popular concert hall for indie and classical music.', '15 Harmony Lane, Culture Hills', 1800,
 'Orchestra Pit, Balcony Seating', 'Instrument Rental Services',
 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),

-- Outdoor Parks
('01H9ZC28AXHEB5J8GB48MKXCFV', 'VENU007', 'Summit Green Park', 'VD007', 'VEN004',
 'Open field ideal for festivals and marathons.', '77 Open Sky Dr, Highland View', 10000,
 'Open Lawn, Picnic Areas, Restroom Stations', 'Zipline Attraction',
 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),

('01H9ZC2A6NRPAZCDYJ0FP6GVPG', 'VENU008', 'Lakeside Event Meadow', 'VD008', 'VEN004',
 'Waterfront park for cultural and wellness events.', '3 Lakeview Blvd, Serenity Cove', 7000,
 'Dock Access, Walking Trails', 'Floating Stage',
 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, TRUE),

-- Theaters
('01H9ZC2C0EFG52WXYQ6E4VRBZR', 'VENU009', 'Royal Theatre', 'VD009', 'VEN005',
 'Classic performing arts venue with ornate interior.', '88 Drama St, Old Town', 1200,
 'Box Seats, Spotlight Systems', 'Stage Automation',
 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),

('01H9ZC2DV8H1F7BRF2YBTR6S9N', 'VENU010', 'Avant Studio Theatre', 'VD010', 'VEN005',
 'Intimate space for experimental and student productions.', '12 Improv Ln, Arts District', 600,
 'Blackbox Layout, Studio Lighting', 'Interactive Sets',
 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),

-- Hotel Ballrooms
('01H9ZC2FJ29K0JYPZQ9MYFHHRY', 'VENU011', 'Crystal Grand Ballroom', 'VD011', 'VEN006',
 'Elegant venue for weddings and formal banquets.', '200 Sapphire Ave, Luxe Hotel', 900,
 'Chandeliers, AV Setup, Buffet Service', 'Live String Quartet',
 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),

('01H9ZC2HBVR2ZK8G8FWYJZF1JN', 'VENU012', 'Skyline Vista Hall', 'VD012', 'VEN006',
 'Top-floor ballroom with city views.', '59 Cloudview Rd, Skytop Inn', 700,
 'Elevator Access, Mood Lighting', 'Fireworks Display',
 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),

-- Exhibition Halls
('01H9ZC2JX5FPGK7M4JEHNHD3NT', 'VENU013', 'TechX Pavilion', 'VD013', 'VEN007',
 'Designed for product launches and robotics expos.', '18 Innovation Blvd, Silicon Heights', 5000,
 'Flexible Booth Layouts, AV Walls', 'Interactive Demo Stations',
 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),

('01H9ZC2L6MK8YXHZTYYDNDWY93', 'VENU014', 'Creators Hub', 'VD014', 'VEN007',
 'Modern hall for creator showcases and maker fairs.', '9 Tinker Way, Maker City', 4500,
 'Modular Panels, Charging Zones', 'Onsite 3D Printing',
 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),

-- Beachfronts
('01H9ZC2NJD9K61WXTCFBX5Z07F', 'VENU015', 'Sunrise Bayfront', 'VD015', 'VEN008',
 'Open beach space ideal for sunrise yoga and ceremonies.', '1 Ocean Breeze Path, Morning Bay', 3000,
 'Changing Rooms, Sand Flooring', 'Live DJ Deck',
 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),

('01H9ZC2PM2KXQGBEWTCKHMKXFN', 'VENU016', 'Wave Pavilion', 'VD016', 'VEN008',
 'Covered beach venue for cocktails and mini festivals.', '15 Shoreline Drive, Coral Reef', 2200,
 'Outdoor Showers, Fire Pit Zones', 'LED Lighting Canopy',
 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),

-- University Auditoriums
('01H9ZC2R42MKE5XVBQHR3M10TZ', 'VENU017', 'Innovation Auditorium', 'VD017', 'VEN009',
 'Lecture and symposium hall with advanced AV.', '9 Research Rd, Tech University', 1500,
 'Projectors, Tiered Seating', 'Simultaneous Translation Booths',
 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),

('01H9ZC2T4EM79KJ2NZW4XMXRD2', 'VENU018', 'Campus Heritage Hall', 'VD018', 'VEN009',
 'Historic hall for graduation ceremonies and speeches.', '55 Scholar Ln, Classic College', 2000,
 'Stage Podium, Arch Seating', 'Historical Tours',
 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),

-- Cultural Centers
('01H9ZC2VW3MDDCFW4J39R5EJYD', 'VENU019', 'Lotus Cultural Hall', 'VD019', 'VEN010',
 'Space for multicultural celebrations and exhibitions.', '7 Harmony Rd, Lotus District', 2500,
 'Display Walls, Auditorium, Cafeteria', 'Traditional Costumes Rental',
 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),

('01H9ZC2X8C3HRYPN60YZWAZW5M', 'VENU020', 'Heritage Grove Pavilion', 'VD020', 'VEN010',
 'Blends nature with history for folk festivals and art shows.', '20 Grove St, Heritage Town', 3000,
 'Natural Landscaping, Amphitheater', 'Craft Market Stalls',
 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, TRUE);

 -- Sample data for Tbl_BusinessOwner

 INSERT INTO Tbl_BusinessOwner (
    BusinessOwnerId, BusinessOwnerCode, Name, Email, PhoneNumber, 
    CreatedBy, CreatedAt, ModifiedBy, ModifiedAt, DeleteFlag
) VALUES
(
    '01H9ZDHJ2XN6DWN1MZ7YB1KZP7', 'BO001', 'Araya Tan', 'araya.tan@tropicalbiz.com', '+66-81-123-4567',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZDHN8BYCTD6V8FWFM89XQM', 'BO002', 'James Nguyen', 'j.nguyen@eventify.vn', '+84-90-765-4321',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZDHQ3ZJPT4X4FJCQMHX6TC', 'BO003', 'Sokha Kem', 'sokha.kem@cambocraft.kh', '+855-12-888-999',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZDHTJFY4F2Q0BRNYB5V98G', 'BO004', 'Manop Li', 'manop.li@chiangfest.co.th', '+66-86-456-1234',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, TRUE
),
(
    '01H9ZDHWZ91VEWG2E5R9Z04KRK', 'BO005', 'Nay Chi Htun', 'naychi@myanmarmoments.com', '+95-9-7654-3210',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
);

-- Sample data for Tbl_TicketType

INSERT INTO Tbl_TicketType (
    TicketTypeId, TicketTypeCode, TicketTypeName,
    CreatedBy, CreatedAt, ModifiedBy, ModifiedAt, DeleteFlag
) VALUES
(
    '01H9ZL1JZ7TVEVX5DZKTRJ7ZDJ', 'TKT001', 'General Admission',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZL1MXY48GEMNJPBGC6AE92', 'TKT002', 'VIP',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZL1Q2JN0Y3HX67FRSBTK93', 'TKT003', 'Student Pass',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZL1SG7HJP7DMTQWJP4S0NR', 'TKT004', 'Early Bird',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZL1V1B2QZHMNXB4Y2KQ6DZ', 'TKT005', 'Group Pass',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZL1XD5A7JB9P9NHRZ3FBJV', 'TKT006', 'Press Access',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZL1Z3RTHFAZW1FYWT52BYY', 'TKT007', 'Backstage Pass',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZL21SM7VAVJ9YMZC6NEDWQ', 'TKT008', 'Online Access',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZL24GJR0RW6P5C0YQ8YZNT', 'TKT009', 'Child Ticket',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZL26ZZS27MBFQMPJRT63T2', 'TKT010', 'Senior Ticket',
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, TRUE
);

-- Sample data for Tbl_Event

INSERT INTO Tbl_Event (
    EventId, EventCode, EventName, CategoryCode, Description, Address,
    StartDate, EndDate, EventImage, IsActive, EventStatus,
    BusinessOwnerCode, TotalTicketQuantity, SoldoutCount,
    CreatedBy, CreatedAt, ModifiedBy, ModifiedAt, DeleteFlag
) VALUES
(
    '01H9ZJK2M7N3DJPVYWEC3TNJNZ', 'EVT001', 'Chiang Music Fest', 'CAT001',
    'An open-air music festival featuring regional and international artists.',
    'Chiang Rai Stadium, Thailand',
    '2025-11-10 17:00:00', '2025-11-12 23:00:00', 'chiang_music_fest.jpg',
    TRUE, 'Scheduled', 'BO001', 5000, 3200,
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZJK4CWPJZXF3ZVA9G9YG8P', 'EVT002', 'TechWave Asia 2025', 'CAT002',
    'A convention for startups and innovators across Southeast Asia.',
    'Bangkok Convention Center, Thailand',
    '2025-08-20 09:00:00', '2025-08-22 18:00:00', 'techwave_asia_2025.png',
    TRUE, 'Scheduled', 'BO002', 10000, 6000,
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZJK64M9Z1WPJ4Y4PPRGTCF', 'EVT003', 'Kem Art Expo', 'CAT003',
    'A three-day celebration of modern and traditional Cambodian art.',
    'Royal Palace Grounds, Phnom Penh',
    '2025-09-15 10:00:00', '2025-09-17 20:00:00', 'kem_art_expo.jpg',
    TRUE, 'Open for Registration', 'BO003', 3000, 900,
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
),
(
    '01H9ZJK7W7HFH2XK3PZ15TCKWY', 'EVT004', 'Chiang Charity Gala', 'CAT004',
    'Annual charity fundraiser with live performances and silent auctions.',
    'Chiang Heritage Hall, Thailand',
    '2025-12-01 18:00:00', '2025-12-01 22:00:00', 'charity_gala_promo.jpg',
    FALSE, 'Canceled', 'BO004', 800, 800,
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, TRUE
),
(
    '01H9ZJK9ZC7QPF2XENKHKMFGQZ', 'EVT005', 'Myanmar Sports Fiesta', 'CAT005',
    'A regional sports tournament featuring teams across ASEAN nations.',
    'Yangon Sports Complex, Myanmar',
    '2025-10-05 08:00:00', '2025-10-07 20:00:00', 'sports_fiesta_banner.jpg',
    TRUE, 'Scheduled', 'BO005', 15000, 13200,
    'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE
);

-- Sample data for Tbl_TicketPrice

INSERT INTO Tbl_TicketPrice (
    TicketPriceId, TicketPriceCode, EventCode, TicketTypeCode,
    TicketPrice, TicketQuantity,
    CreatedBy, CreatedAt, ModifiedBy, ModifiedAt, DeleteFlag
) VALUES
-- Chiang Music Fest
('01H9ZMBT1ZVJZFGKHFBJMP9N3P', 'TP001', 'EVT001', 'TKT001', 499.00, 2000, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZMBV6RVEWBVKZC31K60RQM', 'TP002', 'EVT001', 'TKT002', 1199.00, 500, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZMBXACBYPWBJYY7RMZJGY6', 'TP003', 'EVT001', 'TKT004', 399.00, 1000, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZMBZ9DRGJKH2JRW6ZD2JJZ', 'TP004', 'EVT001', 'TKT005', 449.00, 1500, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, TRUE),

-- TechWave Asia 2025
('01H9ZMC1DRZE7MZQNWJ7E2YRYE', 'TP005', 'EVT002', 'TKT001', 299.00, 4000, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZMC3HZ2BM3WWZDAKPXMM5W', 'TP006', 'EVT002', 'TKT002', 899.00, 1000, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZMC5YTZJ5XK9MPYHBW58N7', 'TP007', 'EVT002', 'TKT006', 0.00, 100, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZMC7X66NW6PKTQ4MGRSVM2', 'TP008', 'EVT002', 'TKT008', 99.00, 2500, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),

-- Kem Art Expo
('01H9ZMC9ZJDE5QGFYXC8N9D6PY', 'TP009', 'EVT003', 'TKT001', 150.00, 1200, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZMCCNGFMBNK5D80HR2W7DQ', 'TP010', 'EVT003', 'TKT003', 100.00, 500, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZMCE3WWCVME90BGJEC2YNW', 'TP011', 'EVT003', 'TKT009', 80.00, 300, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZMCG8PHTHPACXWZV1XM57G', 'TP012', 'EVT003', 'TKT010', 90.00, 200, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, TRUE),

-- Chiang Charity Gala
('01H9ZMCHZBMBXV9ZHXYM64FEHP', 'TP013', 'EVT004', 'TKT001', 200.00, 300, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZMCK6KYKPJWJDNXRY9WK9Z', 'TP014', 'EVT004', 'TKT002', 500.00, 100, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZMCM9CKXBXHDF0AXPGPNYE', 'TP015', 'EVT004', 'TKT007', 600.00, 50, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZMCP2PYJ56WEHEW4JXMJ55', 'TP016', 'EVT004', 'TKT008', 150.00, 200, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, TRUE),

-- Myanmar Sports Fiesta
('01H9ZMCRB9ZF3PF5CMWEG0CNHF', 'TP017', 'EVT005', 'TKT001', 250.00, 6000, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZMCTYT7JQ4C1C13X6GJWFG', 'TP018', 'EVT005', 'TKT005', 225.00, 3000, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZMCWB1ZHD6MRWJED49K0R4', 'TP019', 'EVT005', 'TKT009', 100.00, 2000, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZMCYF9NZX4WJ63MC5S5MPY', 'TP020', 'EVT005', 'TKT010', 120.00, 2000, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE);


-- Sample data for Tbl_Ticket

INSERT INTO Tbl_Ticket (
    TicketId, TicketCode, TicketPriceCode,
    IsUsed, CreatedBy, CreatedAt, ModifiedBy, ModifiedAt, DeleteFlag
) VALUES
('01H9ZN6WZ5MHEVXJ51QYDNQW48', 'TCK001', 'TP001', FALSE, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZN6Y99VJDRW0CHGMK2CTBX', 'TCK002', 'TP001', TRUE,  'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZN71ZHF75Y0AC45BMQKYNB', 'TCK003', 'TP001', FALSE, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZN739G7BPVR9ZE6TYMHHJY', 'TCK004', 'TP001', TRUE,  'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, TRUE),
('01H9ZN75MNHPF9CY7P2KJ10M5Q', 'TCK005', 'TP001', FALSE, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZN775M0RPF9YGA4RDNPVJN', 'TCK006', 'TP001', TRUE,  'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZN7976W79FC4V4XBQHJGZC', 'TCK007', 'TP001', FALSE, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZN7BB85EV6NGE2YJZVKAEZ', 'TCK008', 'TP001', FALSE, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, TRUE),
('01H9ZN7E8VDT05KR1KWY2JYCD6', 'TCK009', 'TP001', TRUE,  'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZN7G92T3DWBKXMNZH28ZMG', 'TCK010', 'TP001', FALSE, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZN7J76WFX7PAH05V6RTQ8K', 'TCK011', 'TP001', TRUE,  'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, TRUE),
('01H9ZN7L0KTDW41RBH2F7ZRWFA', 'TCK012', 'TP001', FALSE, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZN7NC2BQFJ1Z4CVZBJK89V', 'TCK013', 'TP001', TRUE,  'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZN7PP1FRW4BCE5FX4GEHRB', 'TCK014', 'TP001', FALSE, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE),
('01H9ZN7R3Q9WJFRBZWZ63P8YP5', 'TCK015', 'TP001', TRUE,  'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, TRUE),
('01H9ZN7T0XJ0EWY8FWE4XK8N3T', 'TCK016', 'TP001', FALSE, 'admin', CURRENT_TIMESTAMP, 'admin', CURRENT_TIMESTAMP, FALSE);