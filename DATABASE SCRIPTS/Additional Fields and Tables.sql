USE PatientData
GO

ALTER TABLE PatientData..tbPatientMaster ADD Category VARCHAR(100) NULL
GO

CREATE TABLE PxRegNum(
PxID VARCHAR(10),
RxID VARCHAR(10)
)

INSERT INTO PxRegNum(PxID, RxID) VALUES('1','1')
GO

ALTER TABLE PatientData..tbRegistrationDetails ADD Treatment VARCHAR(max) NULL
GO

ALTER TABLE PatientData..tbRegistrationDetails ADD Diagnosis VARCHAR(max) NULL
GO