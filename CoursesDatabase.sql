--CHUYỂN CSDL MASTER
USE master
GO--ĐẢM BẢO CÂU LỆNH TRÊN CHẠY XONG
--KIEM TRA CSDL TỒN TẠI
IF DB_ID('Course') IS NOT NULL
	--XÓA CSDL
	DROP DATABASE Course
GO
--TẠO CSDL 
CREATE DATABASE Course
GO
--DÙNG CSDL 
USE Course
GO
--TẠO BẢNG
CREATE TABLE Users
(
	IDUser CHAR(10),
	Name NVARCHAR(50),
	DOB date,
	TypeUser int,
	IDClass CHAR(10)

	--TẠO PK
	CONSTRAINT PK_Users
	PRIMARY KEY(IDUser)
)

CREATE TABLE Class
(
	IDClass CHAR(10),
	ClassName NVARCHAR(50)

	--TẠO PK
	CONSTRAINT PK_Class
	PRIMARY KEY(IDClass)
)

CREATE TABLE ClassSubject
(
	IDSubject CHAR(10),
	SubjectName NVARCHAR(50),
	--IDExercise CHAR(10),
	--IDUser CHAR(10),
	IDClass CHAR(10)

	--TẠO PK
	CONSTRAINT SubjectClass
	PRIMARY KEY(IDSubject)
)

CREATE TABLE ExerciseSubject
(
	IDExercise CHAR(10),
	IDUser CHAR(10),
	ExerciseName NVARCHAR(50),
	Content NVARCHAR(255),
	CreateDate date,
	DeadLine date,
	IDSubject CHAR(10),	
	Mark float

	--TẠO PK
	CONSTRAINT Exercise
	PRIMARY KEY(IDExercise,IDUser,IDSubject)
)



--TẠO FK
-----USERS
ALTER TABLE Users
ADD
	CONSTRAINT FK_User_Class
	FOREIGN KEY(IDClass)
	REFERENCES Class(IDClass)

------ CLassSubject
ALTER TABLE ClassSubject
ADD
	CONSTRAINT FK_ClassSubject_Class
	FOREIGN KEY(IDClass)
	REFERENCES Class(IDClass)

---- Excercise
ALTER TABLE ExerciseSubject
ADD
	CONSTRAINT FK_ExerciseSubject_ClassSubject
	FOREIGN KEY(IDSubject)
	REFERENCES ClassSubject(IDSubject)

ALTER TABLE ExerciseSubject
ADD
	CONSTRAINT FK_ExerciseSubject_Users
	FOREIGN KEY(IDUser)
	REFERENCES Users(IDUser)
------------------- VALUES ---------------
---- Class_1
insert into Class(IDClass,ClassName)
values('CD1701','Cao đẳng 17CK1')
insert into Class(IDClass,ClassName)
values('CD1702','Cao đẳng 17CK2')
insert into Class(IDClass,ClassName)
values('CD1703','Cao đẳng 17CK3')

---- User_1
insert into Users(IDUser,Name,DOB,TypeUser,IDClass)
values('SV01','Sinh Vien 1','1999-11-25',1,'CD1701')
insert into Users(IDUser,Name,DOB,TypeUser,IDClass)
values('SV02','Sinh Vien 2','1999-11-02',1,'CD1701')
insert into Users(IDUser,Name,DOB,TypeUser,IDClass)
values('SV03','Sinh Vien 3','1999-11-03',1,'CD1702')
insert into Users(IDUser,Name,DOB,TypeUser,IDClass)
values('SV04','Sinh Vien 4','1999-11-04',1,'CD1702')
insert into Users(IDUser,Name,DOB,TypeUser,IDClass)
values('SV05','Sinh Vien 5','1999-11-05',1,'CD1703')
insert into Users(IDUser,Name,DOB,TypeUser,IDClass)
values('SV06','Sinh Vien 6','1999-11-06',1,'CD1703')

---- Subject
insert into ClassSubject
values('LTWCD1701','Lap trinh web 17CK1','CD1701')
insert into ClassSubject
values('LTWCD1702','Lap trinh web 17CK2','CD1702')
insert into ClassSubject
values('LTWCD1703','Lap trinh web 17CK3','CD1703')
insert into ClassSubject
values('OOPCD1701','Lap trinh huong doi tuong 17CK1','CD1701')
insert into ClassSubject
values('OOPCD1702','Lap trinh huong doi tuong 17CK2','CD1702')
insert into ClassSubject
values('OOPCD1703','Lap trinh huong doi tuong 17CK3','CD1703')

---- Exercise
insert into ExerciseSubject
values('BT01','SV01','Bai tap 01','1+1 = ?','2021-03-01','2021-03-30','LTWCD1701',1)
insert into ExerciseSubject
values('BT01','SV02','Bai tap 01','1+1 = ?','2021-03-01','2021-03-30','LTWCD1701',1)
insert into ExerciseSubject
values('BT01','SV03','Bai tap 01','1+1 = ?','2021-03-01','2021-03-30','LTWCD1702',1)
insert into ExerciseSubject
values('BT01','SV04','Bai tap 01','1+1 = ?','2021-03-01','2021-03-30','LTWCD1702',1)
insert into ExerciseSubject
values('BT01','SV05','Bai tap 01','1+1 = ?','2021-03-01','2021-03-30','LTWCD1703',1)
insert into ExerciseSubject
values('BT01','SV06','Bai tap 01','1+1 = ?','2021-03-01','2021-03-30','LTWCD1703',1)

insert into ExerciseSubject
values('BT01','SV01','Bai tap 01','Huong doi tuong la j?','2021-03-01','2021-03-30','OOPCD1701',1)
insert into ExerciseSubject
values('BT01','SV02','Bai tap 01','Huong doi tuong la j?','2021-03-01','2021-03-30','OOPCD1701',1)
insert into ExerciseSubject
values('BT01','SV03','Bai tap 01','Huong doi tuong la j?','2021-03-01','2021-03-30','OOPCD1702',1)
insert into ExerciseSubject
values('BT01','SV04','Bai tap 01','Huong doi tuong la j?','2021-03-01','2021-03-30','OOPCD1702',1)
insert into ExerciseSubject
values('BT01','SV05','Bai tap 01','Huong doi tuong la j?','2021-03-01','2021-03-30','OOPCD1703',1)
insert into ExerciseSubject
values('BT01','SV06','Bai tap 01','Huong doi tuong la j?','2021-03-01','2021-03-30','OOPCD1703',1)

select * from Users
select * from Class
select * from ClassSubject
select * from ExerciseSubject

