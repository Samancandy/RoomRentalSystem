create database RoomRental3;
go
use RoomRental3;
go
create table tbUser(
	id int primary key identity,
	uname varchar(20) unique,
	passw varchar(20))

create table tbVehicle(
	Ve_ID varchar(10) primary key,
	Ve_Type varchar(10),
	Model varchar(20),
	Color varchar(10),
	Power varchar(10),
	Plate varchar(10))

create table tbRoom(
	RoomNo varchar(10) primary key,
	RoomType varchar(20),
	Size varchar(10),
	Price float,
	Status varchar(10))
	   
create table tbPerson(
	PersonID varchar(10) primary key,
	Name varchar(20),
	Gender varchar(8),
	DOB date,
	Phone varchar(10),
	Addr varchar(50))

create table tbVehicleDetails(
	VDID int primary key identity,
	PersonID varchar(10) references tbPerson(PersonID),
	Ve_ID varchar(10) references tbVehicle(Ve_ID))

create table tbRental(
	RentalID varchar(10) primary key,
	PersonID varchar(10) references tbPerson(PersonID),
	RoomNo varchar(10) references tbRoom(RoomNo),
	RentedDate date)

create table tbRentalHistory(
	RHID int primary key identity,
	PersonID varchar(10) references tbPerson(PersonID),
	RoomNo varchar(10) references tbRoom(RoomNo),
	StartDate date,
	EndDate date)

create table tbWorking(
	WorkingID varchar(10) primary key,
	PersonID varchar(10) references tbPerson(PersonID),
	Position varchar(20),
	Salary int,
	StartDate date)

create table tbWorkingHistory(
	WHID varchar(10) primary key,
	PersonID varchar(10) references tbPerson(PersonID),
	WorkingID varchar(10) references tbWorking(WorkingID),
	StartDate date,
	EndDate date)

create table tbWater(
	WaterID varchar(10) primary key,
	WPrevious int not null,
	WCurrent int not null,
	WUsage int,
	Date date not null)

--go
--create trigger trWUsage on tbWater
--after insert as begin
--	declare @ID varchar(10), @Wpre int, @WCur int;
--	select @ID = WaterID, @Wpre = WPrevious, @Wcur = WCurrent from inserted;
--	Update tbWater set WUsage = (@WCur - @Wpre) where WaterID = @ID;
--end
--go
create table tbElectricity(
	ElecID varchar(10) primary key,
	EPrevious int not null,
	ECurrent int not null,
	EUsage int,
	Date date not null)

--go
--create trigger trEUsage on tbElectricity
--after insert as begin
--	declare @ID varchar(10), @Epre int, @Ecur int;
--	select @ID = ElecID, @Epre = EPrevious, @Ecur = ECurrent from inserted;
--	update tbElectricity set EUsage = (@Ecur - @Epre) where ElecID = @ID;
--end
--go
create table tbPayment(
	PaymentID varchar(10) primary key,
	RentalID varchar(10) references tbRental(RentalID),
	WaterID varchar(10) references tbWater(WaterID),
	ElecID varchar(10) references tbElectricity(ElecID),
	Trash float,
	PFVehicle float,
	PaymentDate date)



	insert into tbPerson values('CS00002','Choko', 'Female', '12-09-2018', '0967077002', 'Takeo')
	insert into tbPerson values('CS00001','Candy', 'Male', '12-09-2018', '0967077002', 'Takeo')
	insert into tbPerson values('CS00003','Chehii', 'Female', '12-09-2018', '0967077002', 'Takeo')
	insert into tbRoom values ('RM00001','Medium','4x5', '50', 'Available')
	insert into tbRoom values ('RM00002','Medium','4x5', '50', 'Available')
	insert into tbRoom values ('RM00003','Medium','4x5', '50', 'Available')

	insert into tbUser values('','')

	--select * from tbRoom

	--update tbRoom set RoomType = 'Large', Size = '5x8', Price = '70', Status = 'Available' where RoomNo = 'rm01'

	insert into tbVehicle values('VH00001','bike','Gian', 'red', '', '')
	insert into tbVehicle values('VH00002','bike','Gian', 'White', '', '')

	--select * from tbPerson
	


	--select WorkingID, twg.PersonID, per.Name, Posision, Salary from tbWorking twg inner join tbPerson per on twg.PersonID = per.PersonID where per.Name like '%s%'

	--select * from tbPerson where PersonID like 'wd%'

	insert into tbPerson values('WD00001','MIK', 'Male', '12-09-2018', '0967077002', 'Takeo')
	insert into tbPerson values('WD00002','Mark', 'Male', '12-09-2018', '0967077002', 'Takeo')
	insert into tbPerson values('WD00003','Yon', 'Male', '12-09-2018', '0967077002', 'Takeo')


	insert into tbWorking values('WK00001', 'WD00001', 'Secutiry', 500, '2017/8/13')
	insert into tbWorking values('WK00002', 'WD00003', 'Secutiry', 500, '2017/10/13')
	select top 1 WHID from tbWorkingHistory order by WHID desc














