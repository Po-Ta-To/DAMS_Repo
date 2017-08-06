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

	INSERT INTO Treatment
	Values ('T777', 'Filling and Repairs', 
	'Dental fillings and repairs use restorative materials used to repair teeth which have been compromised due to cavities or trauma', 0);

	INSERT INTO Treatment
	Values ('T888', 'Gum Surgery', 
	'Periodontal or gum disease is an infection that affects the gums and jaw bone, which can lead to a loss of gum and teeth. 
	There are two major stages — gingivitis and periodontitis. Gingivitis is the milder and reversible form; periodontal disease
	is often more severe.', 0);

	INSERT INTO Treatment
	Values ('T999', 'Teeth Whitening', 
	'Teeth naturally darken with age, however staining may be caused by various foods and beverages such as coffee, tea and berries, some drugs such as tetracycline, smoking, or a trauma to a tooth', 0);

	INSERT INTO Treatment
	Values ('T1000', 'Consultation', 
	'A full oral assessment will be conducted to establish your oral health and recommend the best treatments for your mouth if needed', 0);

	INSERT INTO ClinicHospital
	Values ('H111', 'National Dental Centre Singapore',
	'5 Second Hospital Avenue Singapore 168938',
	'Closed on Weekends',
	'63248802',
	'appointment@ndcs.com.sg',
	2);

	INSERT INTO OpeningHours
	Values(1,
	'08:00:00',
	'17:30:00',
	1)

	INSERT INTO OpeningHours
	Values(2,
	'00:00:00',
	'00:00:00',
	1)

	INSERT INTO OpeningHours
	Values(3,
	'00:00:00',
	'00:00:00',
	1)

	INSERT INTO ClinicHospitalTimeslot
	Values(1,
	'9AM - 11AM',
	1)

	INSERT INTO ClinicHospitalTimeslot
	Values(2,
	'1PM - 3PM',
	1)

	INSERT INTO ClinicHospitalTimeslot
	Values(3,
	'3PM - 5PM',
	1)

	INSERT INTO ClinicHospitalTimeslot
	Values(4,
	'',
	1)

	INSERT INTO ClinicHospitalTimeslot
	Values(5,
	'',
	1)

	INSERT INTO ClinicHospital
	Values ('H222', 'About Braces',
	'8, Sinaran Drive, Novena Specialist Centre,#06-01 Singapore 307470',
	'Closed on Sundays and PH',
	'63977177',
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

	INSERT INTO ClinicHospitalTimeslot
	Values(1,
	'AM',
	2)

	INSERT INTO ClinicHospitalTimeslot
	Values(2,
	'PM',
	2)

	INSERT INTO ClinicHospitalTimeslot
	Values(3,
	'',
	2)

	INSERT INTO ClinicHospitalTimeslot
	Values(4,
	'',
	2)

	INSERT INTO ClinicHospitalTimeslot
	Values(5,
	'',
	2)

	INSERT INTO ClinicHospital
	Values ('H333', 'LQ Dental',
	'10 Sinaran Drive #11-20/21, 307506',
	'Closed on Sundays and PH',
	'65380890',
	'askus@lqdent.com.sg',
	2);

	INSERT INTO OpeningHours
	Values(1,
	'09:00:00',
	'19:00:00',
	3)

	INSERT INTO OpeningHours
	Values(2,
	'09:00:00',
	'13:00:00',
	3)

	INSERT INTO OpeningHours
	Values(3,
	'00:00:00',
	'00:00:00',
	3)

	INSERT INTO ClinicHospitalTimeslot
	Values(1,
	'8AM - 10AM',
	3)

	INSERT INTO ClinicHospitalTimeslot
	Values(2,
	'10AM - 12PM',
	3)

	INSERT INTO ClinicHospitalTimeslot
	Values(3,
	'1PM - 3PM',
	3)

	INSERT INTO ClinicHospitalTimeslot
	Values(4,
	'3PM - 5PM',
	3)

	INSERT INTO ClinicHospitalTimeslot
	Values(5,
	'',
	3)

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
	'TomT91@gmail.com');

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
	'Harrylim94@gmail.com');

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


	INSERT INTO [dbo].[AspNetUsers]
	Values ('a22ed221-f68b-45a7-8aa2-b83270bfa30b', 
	'Johnny12@gmail.com',
	0,
	'AJi0YIkbBjrKQSIsTV5NqZ/Eh5yCP9JWeVvJKPF8BQ+MIrIC+FD7X8ar/m8+0lhsBQ==',
	'3b58ac0e-8db6-47bb-a1b0-2e8f5593d6a8',
	NULL,
	0,
	0,
	NULL,
	0,
	0,
	'Johnny12@gmail.com');

	INSERT INTO [dbo].[AspNetUserRoles]
	Values ('a22ed221-f68b-45a7-8aa2-b83270bfa30b', 1);

	INSERT INTO [dbo].[UserAccount]
	Values ('s9731369e',
	'Johnny Teo',
	'1997/07/03',
	'Male',
	'92223333',
	'12 Shine Road',
	'a22ed221-f68b-45a7-8aa2-b83270bfa30b',
	0);



	INSERT INTO [dbo].[AspNetUsers]
	Values ('708c6558-4fb5-4102-8428-d5610fb53697', 
	'RachelL92@gmail.com',
	0,
	'ANNiOmfkn+pH5dOxLJy+3mIMgHeR34Y7s/inZaKjzLHOVIzgsXHvhTRdn/K5xQUIzQ==',
	'f5220f46-b013-4be6-8995-e98e684ba2d7',
	NULL,
	0,
	0,
	NULL,
	0,
	0,
	'RachelL92@gmail.com');

	INSERT INTO [dbo].[AspNetUserRoles]
	Values ('708c6558-4fb5-4102-8428-d5610fb53697', 1);

	INSERT INTO [dbo].[UserAccount]
	Values ('s9211305d',
	'Rachel Lee',
	'1992/01/23',
	'Female',
	'97326462',
	'17 North Avenue',
	'708c6558-4fb5-4102-8428-d5610fb53697',
	0);


	INSERT INTO [dbo].[AspNetUsers]
	Values ('0fc9f461-bac9-4f7d-a3a5-ba91e77aeab7', 
	'MaryG@gmail.com',
	0,
	'AHtu03+5XZaCDs4wl7NxOT+BTSRCVsCesoD+xDT7Iqyfutr5oi2yuH4O6KMF/37VKQ==',
	'd0a0d7a7-cdfe-4677-863c-f0bb5bbec762',
	NULL,
	0,
	0,
	NULL,
	0,
	0,
	'MaryG@gmail.com');

	INSERT INTO [dbo].[AspNetUserRoles]
	Values ('0fc9f461-bac9-4f7d-a3a5-ba91e77aeab7', 1);

	INSERT INTO [dbo].[UserAccount]
	Values ('s9821276f',
	'Mary Goh',
	'1998/04/27',
	'Female',
	'83672881',
	'Serangoon Ave 3',
	'0fc9f461-bac9-4f7d-a3a5-ba91e77aeab7',
	0);




	INSERT INTO [dbo].[DoctorDentist]
	Values('D111',
	'Dr Andrew Tay',
	14,
	1);


	INSERT INTO [dbo].[DoctorDentist]
	Values('D222',
	'Dr Adrian Shi',
	15,
	1);

	INSERT INTO [dbo].[DoctorDentist]
	Values('D333',
	'Dr Stefan Vaz',
	10,
	2);

	INSERT INTO [dbo].[DoctorDentist]
	Values('D444',
	'Dr Agnes Wong',
	11,
	3);

	INSERT INTO [dbo].[DoctorDentist]
	Values('D555',
	'Dr Claire Chen',
	12,
	3);

	INSERT INTO [dbo].[DoctorDentist]
	Values('D666',
	'Dr Darren Lee',
	13,
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

	INSERT INTO [dbo].[ClinicHospitalTreatment]
	Values(3,
	3,
	2100,
	4500);


	INSERT INTO [dbo].[ClinicHospitalTreatment]
	Values(4,
	3,
	300,
	2000);

	INSERT INTO [dbo].[ClinicHospitalTreatment]
	Values(5,
	1,
	600,
	800);

	INSERT INTO [dbo].[ClinicHospitalTreatment]
	Values(6,
	3,
	75,
	650);

	INSERT INTO [dbo].[ClinicHospitalTreatment]
	Values(7,
	1,
	110,
	240);

	INSERT INTO [dbo].[ClinicHospitalTreatment]
	Values(8,
	3,
	500,
	9999);

	INSERT INTO [dbo].[ClinicHospitalTreatment]
	Values(9,
	2,
	650,
	650);


	--INSERT INTO [dbo].[Bookings]
	--Values('20171230',
	--1,
	--1);

	--INSERT INTO [dbo].[Bookings]
	--Values('20171121',
	--0,
	--2);

	--INSERT INTO [dbo].[Bookings]
	--Values('20171014',
	--0,
	--3);

	INSERT INTO [dbo].[Appointment]
	Values('AP1',
	1,
	1,
	1,
	'20170828',
	1,
	1,
	1,
	'Allergic to painkillers',
	'20170828',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP2',
	2,
	2,
	1,
	'20170912',
	1,
	3,
	3,
	'No Remarks',
	'20170913',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP3',
	2,
	3,
	1,
	'20171020',
	1,
	5,
	4,
	'No Remarks',
	'20171019',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP4',
	1,
	1,
	1,
	'20171220',
	1,
	1,
	2,
	'No Remarks',
	'20171220',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP5',
	3,
	1,
	1,
	'20170909',
	1,
	2,
	1,
	'Appointment time has to be after 1900',
	'20170909',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP6',
	3,
	2,
	1,
	'20170825',
	1,
	3,
	3,
	'Appointment time to be in the afternoon',
	'20170825',
	1);


	INSERT INTO [dbo].[Appointment]
	Values('AP7',
	4,
	3,
	1,
	'20171001',
	1,
	5,
	5,
	'No Remarks',
	'20171002',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP8',
	4,
	2,
	1,
	'20171106',
	1,
	3,
	3,
	'Appointment time has to be in morning',
	'20171106',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP9',
	5,
	3,
	1,
	'20171214',
	1,
	5,
	6,
	'No Remarks',
	'20171213',
	1);


	INSERT INTO [dbo].[Appointment]
	Values('AP10',
	5,
	1,
	1,
	'20170731',
	1,
	2,
	1,
	'No Remarks',
	'20170801',
	1);



	---------11-20
	INSERT INTO [dbo].[Appointment]
	Values('AP11',
	1,
	1,
	1,
	'20170828',
	1,
	1,
	1,
	'Allergic to painkillers',
	'20170828',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP12',
	2,
	2,
	1,
	'20170912',
	1,
	3,
	3,
	'No Remarks',
	'20170913',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP13',
	2,
	3,
	1,
	'20171020',
	1,
	5,
	4,
	'No Remarks',
	'20171019',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP14',
	1,
	1,
	1,
	'20171220',
	1,
	1,
	2,
	'No Remarks',
	'20171220',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP15',
	3,
	1,
	1,
	'20170909',
	1,
	2,
	1,
	'Appointment time has to be after 1900',
	'20170909',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP16',
	3,
	2,
	1,
	'20170825',
	1,
	3,
	3,
	'Appointment time to be in the afternoon',
	'20170825',
	1);


	INSERT INTO [dbo].[Appointment]
	Values('AP17',
	4,
	3,
	1,
	'20171001',
	1,
	5,
	5,
	'No Remarks',
	'20171002',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP18',
	4,
	2,
	1,
	'20171106',
	1,
	3,
	3,
	'Appointment time has to be in morning',
	'20171106',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP19',
	5,
	3,
	1,
	'20171214',
	1,
	5,
	6,
	'No Remarks',
	'20171213',
	1);


	INSERT INTO [dbo].[Appointment]
	Values('AP20',
	5,
	1,
	1,
	'20170731',
	1,
	2,
	1,
	'No Remarks',
	'20170801',
	1);



	------21-30
	INSERT INTO [dbo].[Appointment]
	Values('AP21',
	1,
	1,
	1,
	'20170828',
	1,
	1,
	1,
	'Allergic to painkillers',
	'20170828',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP22',
	2,
	2,
	1,
	'20170912',
	1,
	3,
	3,
	'No Remarks',
	'20170913',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP23',
	2,
	3,
	1,
	'20171020',
	1,
	5,
	4,
	'No Remarks',
	'20171019',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP24',
	1,
	1,
	1,
	'20171220',
	1,
	1,
	2,
	'No Remarks',
	'20171220',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP25',
	3,
	1,
	1,
	'20170909',
	1,
	2,
	1,
	'Appointment time has to be after 1900',
	'20170909',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP26',
	3,
	2,
	1,
	'20170825',
	1,
	3,
	3,
	'Appointment time to be in the afternoon',
	'20170825',
	1);


	INSERT INTO [dbo].[Appointment]
	Values('AP27',
	4,
	3,
	1,
	'20171001',
	1,
	5,
	5,
	'No Remarks',
	'20171002',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP28',
	4,
	2,
	1,
	'20171106',
	1,
	3,
	3,
	'Appointment time has to be in morning',
	'20171106',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP29',
	5,
	3,
	1,
	'20171214',
	1,
	5,
	6,
	'No Remarks',
	'20171213',
	1);


	INSERT INTO [dbo].[Appointment]
	Values('AP30',
	5,
	1,
	1,
	'20170731',
	1,
	2,
	1,
	'No Remarks',
	'20170801',
	1);



	-----31-40
	INSERT INTO [dbo].[Appointment]
	Values('AP31',
	1,
	1,
	1,
	'20170828',
	1,
	1,
	1,
	'Allergic to painkillers',
	'20170828',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP32',
	2,
	2,
	1,
	'20170912',
	1,
	3,
	3,
	'No Remarks',
	'20170913',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP33',
	2,
	3,
	1,
	'20171020',
	1,
	5,
	4,
	'No Remarks',
	'20171019',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP34',
	1,
	1,
	1,
	'20171220',
	1,
	1,
	2,
	'No Remarks',
	'20171220',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP35',
	3,
	1,
	1,
	'20170909',
	1,
	2,
	1,
	'Appointment time has to be after 1900',
	'20170909',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP36',
	3,
	2,
	1,
	'20170825',
	1,
	3,
	3,
	'Appointment time to be in the afternoon',
	'20170825',
	1);


	INSERT INTO [dbo].[Appointment]
	Values('AP37',
	4,
	3,
	1,
	'20171001',
	1,
	5,
	5,
	'No Remarks',
	'20171002',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP38',
	4,
	2,
	1,
	'20171106',
	1,
	3,
	3,
	'Appointment time has to be in morning',
	'20171106',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP39',
	5,
	3,
	1,
	'20171214',
	1,
	5,
	6,
	'No Remarks',
	'20171213',
	1);


	INSERT INTO [dbo].[Appointment]
	Values('AP40',
	5,
	1,
	1,
	'20170731',
	1,
	2,
	1,
	'No Remarks',
	'20170801',
	1);


	-----41-50
	INSERT INTO [dbo].[Appointment]
	Values('AP41',
	1,
	1,
	1,
	'20170828',
	1,
	1,
	1,
	'Allergic to painkillers',
	'20170828',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP42',
	2,
	2,
	1,
	'20170912',
	1,
	3,
	3,
	'No Remarks',
	'20170913',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP43',
	2,
	3,
	1,
	'20171020',
	1,
	5,
	4,
	'No Remarks',
	'20171019',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP44',
	1,
	1,
	1,
	'20171220',
	1,
	1,
	2,
	'No Remarks',
	'20171220',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP45',
	3,
	1,
	1,
	'20170909',
	1,
	2,
	1,
	'Appointment time has to be after 1900',
	'20170909',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP46',
	3,
	2,
	1,
	'20170825',
	1,
	3,
	3,
	'Appointment time to be in the afternoon',
	'20170825',
	1);


	INSERT INTO [dbo].[Appointment]
	Values('AP47',
	4,
	3,
	1,
	'20171001',
	1,
	5,
	5,
	'No Remarks',
	'20171002',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP48',
	4,
	2,
	1,
	'20171106',
	1,
	3,
	3,
	'Appointment time has to be in morning',
	'20171106',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP49',
	5,
	3,
	1,
	'20171214',
	1,
	5,
	6,
	'No Remarks',
	'20171213',
	1);


	INSERT INTO [dbo].[Appointment]
	Values('AP50',
	5,
	1,
	1,
	'20170731',
	1,
	2,
	1,
	'No Remarks',
	'20170801',
	1);



	---51-60
	INSERT INTO [dbo].[Appointment]
	Values('AP51',
	1,
	1,
	1,
	'20170828',
	1,
	1,
	1,
	'Allergic to painkillers',
	'20170828',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP52',
	2,
	2,
	1,
	'20170912',
	1,
	3,
	3,
	'No Remarks',
	'20170913',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP53',
	2,
	3,
	1,
	'20171020',
	1,
	5,
	4,
	'No Remarks',
	'20171019',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP54',
	1,
	1,
	1,
	'20171220',
	1,
	1,
	2,
	'No Remarks',
	'20171220',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP55',
	3,
	1,
	1,
	'20170909',
	1,
	2,
	1,
	'Appointment time has to be after 1900',
	'20170909',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP56',
	3,
	2,
	1,
	'20170825',
	1,
	3,
	3,
	'Appointment time to be in the afternoon',
	'20170825',
	1);


	INSERT INTO [dbo].[Appointment]
	Values('AP57',
	4,
	3,
	1,
	'20171001',
	1,
	5,
	5,
	'No Remarks',
	'20171002',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP58',
	4,
	2,
	1,
	'20171106',
	1,
	3,
	3,
	'Appointment time has to be in morning',
	'20171106',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP59',
	5,
	3,
	1,
	'20171214',
	1,
	5,
	6,
	'No Remarks',
	'20171213',
	1);


	INSERT INTO [dbo].[Appointment]
	Values('AP60',
	5,
	1,
	1,
	'20170731',
	1,
	2,
	1,
	'No Remarks',
	'20170801',
	1);


	----61-70
	INSERT INTO [dbo].[Appointment]
	Values('AP61',
	1,
	1,
	1,
	'20170828',
	1,
	1,
	1,
	'Allergic to painkillers',
	'20170828',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP62',
	2,
	2,
	1,
	'20170912',
	1,
	3,
	3,
	'No Remarks',
	'20170913',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP63',
	2,
	3,
	1,
	'20171020',
	1,
	5,
	4,
	'No Remarks',
	'20171019',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP64',
	1,
	1,
	1,
	'20171220',
	1,
	1,
	2,
	'No Remarks',
	'20171220',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP65',
	3,
	1,
	1,
	'20170909',
	1,
	2,
	1,
	'Appointment time has to be after 1900',
	'20170909',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP66',
	3,
	2,
	1,
	'20170825',
	1,
	3,
	3,
	'Appointment time to be in the afternoon',
	'20170825',
	1);


	INSERT INTO [dbo].[Appointment]
	Values('AP67',
	4,
	3,
	1,
	'20171001',
	1,
	5,
	5,
	'No Remarks',
	'20171002',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP68',
	4,
	2,
	1,
	'20171106',
	1,
	3,
	3,
	'Appointment time has to be in morning',
	'20171106',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP69',
	5,
	3,
	1,
	'20171214',
	1,
	5,
	6,
	'No Remarks',
	'20171213',
	1);


	INSERT INTO [dbo].[Appointment]
	Values('AP70',
	5,
	1,
	1,
	'20170731',
	1,
	2,
	1,
	'No Remarks',
	'20170801',
	1);


	----71-80
	INSERT INTO [dbo].[Appointment]
	Values('AP71',
	1,
	1,
	1,
	'20170828',
	1,
	1,
	1,
	'Allergic to painkillers',
	'20170828',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP72',
	2,
	2,
	1,
	'20170912',
	1,
	3,
	3,
	'No Remarks',
	'20170913',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP73',
	2,
	3,
	1,
	'20171020',
	1,
	5,
	4,
	'No Remarks',
	'20171019',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP74',
	1,
	1,
	1,
	'20171220',
	1,
	1,
	2,
	'No Remarks',
	'20171220',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP75',
	3,
	1,
	1,
	'20170909',
	1,
	2,
	1,
	'Appointment time has to be after 1900',
	'20170909',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP76',
	3,
	2,
	1,
	'20170825',
	1,
	3,
	3,
	'Appointment time to be in the afternoon',
	'20170825',
	1);


	INSERT INTO [dbo].[Appointment]
	Values('AP77',
	4,
	3,
	1,
	'20171001',
	1,
	5,
	5,
	'No Remarks',
	'20171002',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP78',
	4,
	2,
	1,
	'20171106',
	1,
	3,
	3,
	'Appointment time has to be in morning',
	'20171106',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP79',
	5,
	3,
	1,
	'20171214',
	1,
	5,
	6,
	'No Remarks',
	'20171213',
	1);


	INSERT INTO [dbo].[Appointment]
	Values('AP80',
	5,
	1,
	1,
	'20170731',
	1,
	2,
	1,
	'No Remarks',
	'20170801',
	1);


	----81-90
	INSERT INTO [dbo].[Appointment]
	Values('AP81',
	1,
	1,
	1,
	'20170828',
	1,
	1,
	1,
	'Allergic to painkillers',
	'20170828',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP82',
	2,
	2,
	1,
	'20170912',
	1,
	3,
	3,
	'No Remarks',
	'20170913',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP83',
	2,
	3,
	1,
	'20171020',
	1,
	5,
	4,
	'No Remarks',
	'20171019',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP84',
	1,
	1,
	1,
	'20171220',
	1,
	1,
	2,
	'No Remarks',
	'20171220',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP85',
	3,
	1,
	1,
	'20170909',
	1,
	2,
	1,
	'Appointment time has to be after 1900',
	'20170909',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP86',
	3,
	2,
	1,
	'20170825',
	1,
	3,
	3,
	'Appointment time to be in the afternoon',
	'20170825',
	1);


	INSERT INTO [dbo].[Appointment]
	Values('AP87',
	4,
	3,
	1,
	'20171001',
	1,
	5,
	5,
	'No Remarks',
	'20171002',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP88',
	4,
	2,
	1,
	'20171106',
	1,
	3,
	3,
	'Appointment time has to be in morning',
	'20171106',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP89',
	5,
	3,
	1,
	'20171214',
	1,
	5,
	6,
	'No Remarks',
	'20171213',
	1);


	INSERT INTO [dbo].[Appointment]
	Values('AP90',
	5,
	1,
	1,
	'20170731',
	1,
	2,
	1,
	'No Remarks',
	'20170801',
	1);


	---91-100
	INSERT INTO [dbo].[Appointment]
	Values('AP91',
	1,
	1,
	1,
	'20170828',
	1,
	1,
	1,
	'Allergic to painkillers',
	'20170828',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP92',
	2,
	2,
	1,
	'20170912',
	1,
	3,
	3,
	'No Remarks',
	'20170913',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP93',
	2,
	3,
	1,
	'20171020',
	1,
	5,
	4,
	'No Remarks',
	'20171019',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP94',
	1,
	1,
	1,
	'20171220',
	1,
	1,
	2,
	'No Remarks',
	'20171220',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP95',
	3,
	1,
	1,
	'20170909',
	1,
	2,
	1,
	'Appointment time has to be after 1900',
	'20170909',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP96',
	3,
	2,
	1,
	'20170825',
	1,
	3,
	3,
	'Appointment time to be in the afternoon',
	'20170825',
	1);


	INSERT INTO [dbo].[Appointment]
	Values('AP97',
	4,
	3,
	1,
	'20171001',
	1,
	5,
	5,
	'No Remarks',
	'20171002',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP98',
	4,
	2,
	1,
	'20171106',
	1,
	3,
	3,
	'Appointment time has to be in morning',
	'20171106',
	1);

	INSERT INTO [dbo].[Appointment]
	Values('AP99',
	5,
	3,
	1,
	'20171214',
	1,
	5,
	6,
	'No Remarks',
	'20171213',
	1);


	INSERT INTO [dbo].[Appointment]
	Values('AP100',
	5,
	1,
	1,
	'20170731',
	1,
	2,
	1,
	'No Remarks',
	'20170801',
	1);
