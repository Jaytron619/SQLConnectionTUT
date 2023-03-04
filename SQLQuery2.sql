create table studentFees
(
 Student_Id varchar(50) not null,
 Admission_Fee varchar(50) null,
 Semester_Fee varchar(50) null,
 Fine varchar(50) null,
 Previous_Balance varchar(50) null,
 TOtal_Fee varchar(50) null,
 Total_Payed varchar(50) null ,
 PaymentDate varchar(50) null,
 Balance varchar(50) null,


 constraint fk_StudentID 
 foreign key (Student_Id)
 references studentAdmission (Student_Id)
)