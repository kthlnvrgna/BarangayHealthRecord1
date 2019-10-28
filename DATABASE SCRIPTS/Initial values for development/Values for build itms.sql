INSERT INTO PatientData..tbPatientMaster
(FirstName, MiddleName, Lastname, Address, Sex, Nationality, Religion, BirthDate, CivilStatus)
VALUES
('Original', 'Glazed', 'Donut', 'Krispy Krme', 'F', '1', '1', CAST(GETDATE() AS DATE), '1')
 
INSERT INTO MasterFile..tbNationality
(Descripton)
VALUES
('French')

INSERT INTO MasterFile..tbNationality
(Descripton)
VALUES
('Filipino')

INSERT INTO MasterFile..tbReligion
(Descripton)
VALUES
('Roman Catholic')

INSERT INTO MasterFile..tbCivilStatus
(Descripton) VALUES ('Single')
INSERT INTO MasterFile..tbCivilStatus
(Descripton) VALUES ('Married')
INSERT INTO MasterFile..tbCivilStatus
(Descripton) VALUES ('Widowed')
INSERT INTO MasterFile..tbCivilStatus
(Descripton) VALUES ('Separated')
