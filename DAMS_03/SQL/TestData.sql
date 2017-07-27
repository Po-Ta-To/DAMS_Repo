Use DAMS_01
Go


INSERT INTO Treatment
Values ('T111', 'Bonding', 
'Applying composite tooth bonding is a restorative procedure that uses tooth enamel-coloured composite resin (plastic)
to repair teeth that are decayed, chipped, fractured or discoloured. Tooth gaps can also be closed. Unlike veneers, which
require laboratory work, bonding is done in the dental office', 0)

INSERT INTO Treatment
Values ('T222', 'Braces', 
'A dental brace is a device used to correct the alignment of teeth and bite-related problems (including underbite,
overbite, etc.). Braces straighten teeth by exerting steady pressure on the teeth.', 1);

INSERT INTO Treatment
Values ('T333', 'Bridges and Implants', 
'Bridges and implants are two ways to replace a missing tooth or teeth. Bridges are false anchored in place by neighbouring
teeth. The bridge consists of two crowns on the anchoring teeth along with the false tooth in the centre. Dental implants are
artificial roots used to support replacement teeth.', 1);

INSERT INTO Treatment
Values ('T444', 'Crowns and Caps', 
'Crowns are dental restorations that protect damaged, cracked or broken teeth. Dental crowns, often referred to as caps, 
sit over the entire part of the tooth that lies above the gum line.', 0);

INSERT INTO Treatment
Values ('T555', 'Dentures ', 
'Dentures are prosthetic devices replacing lost teeth. There are two types of dentures – partial and full.
Full dentures are often referred to as “false teeth”', 0);

INSERT INTO Treatment
Values ('T666', 'Extractions', 
'A severely damaged tooth may need to be extracted. Permanent teeth may also need to be removed for orthodontic treatment.', 0);

INSERT INTO ClinicHospital
Values ('H111', 'National Dental Centre Singapore',
'5 Second Hospital Avenue Singapore 168938',
'mon-fri',
'6324 8802',
'appointment@ndcs.com.sg',
1);

INSERT INTO OpeningHours
Values(1,
'08:00:00',
'17:30:00',
1)

--INSERT INTO OpeningHours
--Values(2,
--'00:00:00',
--'00:00:00',
--1)

--INSERT INTO OpeningHours
--Values(3,
--'00:00:00',
--'00:00:00',
--1)

INSERT INTO ClinicHospital
Values ('H222', 'About Braces',
'8, Sinaran Drive, Novena Specialist Centre,#06-01 Singapore 307470',
'mon, tue-fri, sat',
'6397 7177',
'enquiry@aboutbraces.org',
1);

INSERT INTO OpeningHours
Values(1,
'13:00:00',
'18:00:00',
2)

INSERT INTO OpeningHours
Values(2,
'08:00:00',
'18:00:00',
2)

INSERT INTO OpeningHours
Values(3,
'08:00:00',
'13:30:00',
2)

INSERT INTO ClinicHospital
Values ('H333', 'Mount Elizabeth Hospital',
'3 Mount Elizabeth, Singapore 228510',
'24 Hours Daily',
'6737 2666',
'HospThree@hosp.com',
1);

INSERT INTO OpeningHours
Values(1,
'00:00:00',
'23:59:00',
3)

--INSERT INTO OpeningHours
--Values(2,
--'00:00:00',
--'00:00:00',
--3)

--INSERT INTO OpeningHours
--Values(3,
--'00:00:00',
--'00:00:00',
--3)

INSERT INTO [dbo].[AspNetUsers]
Values ('2a6e9769-165b-4ca1-939f-7b32c1402835', 
'ClerkOne@gmail.com',
0,
'ALNA19rwVg69eJTmIFene5eTEJgCmyZyaebAuxycEQyGftAMwkKYUWLjKrfN007ATw==',
'511459dc-bffc-4c14-8207-28aa0c4ddeff',
NULL,
0,
0,
NULL,
0,
0,
'C111');

INSERT INTO [dbo].[AspNetUserRoles]
Values ('2a6e9769-165b-4ca1-939f-7b32c1402835', 4);

INSERT INTO [dbo].[AdminAccount]
Values ('OrgC111',
'ClerkOne',
'ClerkOne@gmail.com',
3,
'2a6e9769-165b-4ca1-939f-7b32c1402835');

INSERT INTO [dbo].[AdminAccountClinicHospital]
Values (
1, 2);


INSERT INTO [dbo].[AspNetUsers]
Values ('4cc453e5-c9e0-4cea-8275-e93807fc92fe', 
'ClerkTwo@gmail.com',
0,
'AM3vvyf9DfFZSZHaFbO3fN6cRQIkKyfAjuof+qa5Qnt053b+XKrB1Ql4k92z8jut1w==',
'fe1abd5b-94bd-4429-b4ad-6c2957645e83',
NULL,
0,
0,
NULL,
0,
0,
'C222');

INSERT INTO [dbo].[AspNetUserRoles]
Values ('4cc453e5-c9e0-4cea-8275-e93807fc92fe', 4);

INSERT INTO [dbo].[AdminAccount]
Values ('OrgC222',
'ClerkTwo',
'ClerkTwo@gmail.com',
3,
'4cc453e5-c9e0-4cea-8275-e93807fc92fe');

INSERT INTO [dbo].[AdminAccountClinicHospital]
Values (
1, 3);

INSERT INTO [dbo].[AspNetUsers]
Values ('8b107064-b293-4745-9a26-fe3ca10f1d0f', 
'ClerkThree@gmail.com',
0,
'AHRuR0+GgT0mts0VyaYAFlRjdS/ZB00amyZaKQp6CzzlRmmlnof4uICQEkd85fsjdw==',
'b4d89d5d-b81d-4491-981a-0d8d8c68106e',
NULL,
0,
0,
NULL,
0,
0,
'C333');

INSERT INTO [dbo].[AspNetUserRoles]
Values ('8b107064-b293-4745-9a26-fe3ca10f1d0f', 4);

INSERT INTO [dbo].[AdminAccount]
Values ('OrgC333',
'ClerkThree',
'ClerkThree@gmail.com',
3,
'8b107064-b293-4745-9a26-fe3ca10f1d0f');

INSERT INTO [dbo].[AdminAccountClinicHospital]
Values (
2, 4);


INSERT INTO [dbo].[AspNetUsers]
Values ('ea97021a-8eb8-4a9d-af45-f2244634d79c', 
'AdminOne@gmail.com',
0,
'AGJtmqi6/x6r1yjh9wL3YpWL/gjHpN1nTSkeQSGkKpB9E1vc1clxcoAY0GIpNT/K3A==',
'b6746ac9-abf0-4e04-94f5-04d913eada86',
NULL,
0,
0,
NULL,
0,
0,
'A111');

INSERT INTO [dbo].[AspNetUserRoles]
Values ('ea97021a-8eb8-4a9d-af45-f2244634d79c', 3);

INSERT INTO [dbo].[AdminAccount]
Values ('OrgA111',
'AdminOne',
'AdminOne@gmail.com',
2,
'ea97021a-8eb8-4a9d-af45-f2244634d79c');

INSERT INTO [dbo].[AdminAccountClinicHospital]
Values (
1, 5);


INSERT INTO [dbo].[AspNetUsers]
Values ('ceead7d6-e5b7-44a1-8f47-47e003d349a1', 
'AdminTwo@gmail.com',
0,
'ADBUA3xdIVBe3s77NKQyFQzyILs2w6b6RwiNkNfjhAl99Dm/G3bU9RLRa+Qp3LNe3A==',
'dde4d02c-fbe3-48f1-b2f9-54b5225d51ec',
NULL,
0,
0,
NULL,
0,
0,
'A222');

INSERT INTO [dbo].[AspNetUserRoles]
Values ('ceead7d6-e5b7-44a1-8f47-47e003d349a1', 3);

INSERT INTO [dbo].[AdminAccount]
Values ('OrgA222',
'AdminTwo',
'AdminTwo@gmail.com',
2,
'ceead7d6-e5b7-44a1-8f47-47e003d349a1');

INSERT INTO [dbo].[AdminAccountClinicHospital]
Values (
2, 6);



INSERT INTO [dbo].[AspNetUsers]
Values ('82c2540d-ef60-4681-9a0b-abbfad07339f', 
'TomT91@gmail.com',
0,
'AKxueI098MrJGAhs+W66iW5el55ipTcmRIF0VD3uzHrd/5j5O8OFTkbQP7MdIq2B4Q==',
'ddec61d2-c492-466f-92f8-9f3fd3029621',
NULL,
0,
0,
NULL,
0,
0,
'U111');

INSERT INTO [dbo].[AspNetUserRoles]
Values ('82c2540d-ef60-4681-9a0b-abbfad07339f', 1);

INSERT INTO [dbo].[UserAccount]
Values ('S9125721A',
'Tommy Tan',
'January 30, 1991',
'Male',
'97275398',
'2 Ang Mo Kio Avenue',
'82c2540d-ef60-4681-9a0b-abbfad07339f',
0);

INSERT INTO [dbo].[AspNetUsers]
Values ('009115f3-68ed-4215-bba7-d48551dc4e5f', 
'Harrylim94@gmail.com',
0,
'ANgyghUp901j/H0PnM4MgFXB+8895RtnDutvyDdRnRtvOxwGSMq4iub0ZpbVSs/+FQ==',
'002ff1ee-5134-4528-b552-acd41f7f470f',
NULL,
0,
0,
NULL,
0,
0,
'U222');

INSERT INTO [dbo].[AspNetUserRoles]
Values ('009115f3-68ed-4215-bba7-d48551dc4e5f', 1);

INSERT INTO [dbo].[UserAccount]
Values ('S9421207G',
'Harry Lim',
'May 15, 1994',
'Male',
'84236923',
'1 Tai Seng Drive',
'009115f3-68ed-4215-bba7-d48551dc4e5f',
0);




INSERT INTO [dbo].[DoctorDentist]
Values('D111',
'Dr Vaz',
10,
2);

INSERT INTO [dbo].[DoctorDentist]
Values('D222',
'Dr Audrey Tan',
11,
3);

INSERT INTO [dbo].[DoctorDentist]
Values('D333',
'Dr Alfred Cheng',
12,
3);

INSERT INTO [dbo].[ClinicHospitalTreatment]
Values(1,
1,
300,
600);

INSERT INTO [dbo].[ClinicHospitalTreatment]
Values(2,
2,
3500,
6000);

INSERT INTO [dbo].[ClinicHospitalTreatment]
Values(2,
1,
4500,
6000);

INSERT INTO [dbo].[Bookings]
Values('20171230',
1,
1);

INSERT INTO [dbo].[Bookings]
Values('20171121',
0,
2);

INSERT INTO [dbo].[Appointment]
Values('AP111',
1,
1,
1,
'20170828',
1400,
1,
1,
'Allergic to painkillers',
'20170828',
1330);