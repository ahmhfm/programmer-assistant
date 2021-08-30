create database visualStudio;
go

ALTER DATABASE visualStudio COLLATE Arabic_CI_AS;  -- تغيير الترميز او الكوليجن لتتعرف قاعدة البيانات على اللغة العربية
GO

----use master
----go
----alter database visualStudio set single_user with rollback immediate
----drop database visualStudio

use visualStudio;
go

--***************************************************************************************************************************************************
-- جدول العناصر
create table element
(
element_no int identity primary key,
element_name nvarchar(100),
element_description nvarchar(500)
)

go
--insert into element values ('textBox','يتم الكتابة فيه')
--insert into element values ('comboBox','قائمة منسدلة')
--insert into element values ('listBox','قائمة عناصر')
--insert into element values ('timer','يقوم بحساب وقت العمليات')

--update element set element_name ='5',element_description='5' where element_no=5
--***************************************************************************************************************************************************
-- جدول الاكواد
create table code
(
code_no int identity primary key,
code_elementNo int references element(element_no),
code_code nvarchar(500),
code_description nvarchar(500)
)

go
--***************************************************************************************************************************************************
-- جدول الشرح
create table Explanation
(
Explanation_no int identity primary key,
Explanation_elementNo int references element(element_no),
Explanation_Title nvarchar(100),
Explanation_Explanation nvarchar(1000)
)

go
--***************************************************************************************************************************************************
-- جدول مجموعات الخصائص
create table propertyGroup
(
propertyGroup_no int identity primary key,
propertyGroup_name nvarchar(100),
propertyGroup_description nvarchar(500)
)

go
--insert into propertyGroup values ('Appearance','المظهر الخارجي');
--insert into propertyGroup values ('Behavior','السلوك');
--insert into propertyGroup values ('Data','البيانات');
--insert into propertyGroup values ('Design','التصميم');
--***************************************************************************************************************************************************
-- جدول الخصائص
create table property
(
property_no int identity primary key,
property_name nvarchar(100),
property_description nvarchar(500),
property_propertyGroupNo int references propertyGroup(propertyGroup_no) on update cascade
)

go
--insert into property values ('Back Color','لون الخلفية',1);
--insert into property values ('Cursor','المؤشر',1);
--insert into property values ('Allow Drop','السماح بالهدم',2);
--insert into property values ('Auto Validate','تحقق الي',2);
--insert into property values ('Enabled','سماح',2);
--insert into property values ('Tag','بطاقة شعار',3);
--***************************************************************************************************************************************************
-- جدول مجموعات الاحداث
create table eventGroup
(
eventGroup_no int identity primary key,
eventGroup_name nvarchar(100),
eventGroup_description nvarchar(500)
)
go
--insert into eventGroup values ('action','اجراء');
--insert into eventGroup values ('drag drop','سحب وافلات');
--insert into eventGroup values ('focus','تركيز');
--***************************************************************************************************************************************************
-- جدول الاحداث
create table eevent
(
eevent_no int identity primary key,
eevent_name nvarchar(100),
eevent_description nvarchar(500),
eevent_eventGroupNo int references eventGroup(eventGroup_no) on update cascade
)
go
--insert into eevent values ('click','ضغط',1);
--insert into eevent values ('dragEnter','ادخال سحب',2);
--insert into eevent values ('MouseClick','ضغط فأرة',1);
--insert into eevent values ('Enter','ادخال',3);
--***************************************************************************************************************************************************
-- جدل ربط العناصر بالخصائص
create table linkElementWithProperty
(
linkElementWithProperty_elementNo int references element(element_no) on update cascade ,
linkElementWithProperty_propertyNo int references property(property_no) on update cascade ,
linkElementWithProperty_description nvarchar(500),
primary key (linkElementWithProperty_elementNo,linkElementWithProperty_propertyNo)
)
go
--insert into linkElementWithProperty values (1,1,'لون خلفية حقل النص');
--insert into linkElementWithProperty values (1,2,'لون خلفية حقل النص');
--insert into linkElementWithProperty values (2,6,'لون خلفية حقل النص');
--insert into linkElementWithProperty values (2,3,'لون خلفية حقل النص');
--insert into linkElementWithProperty values (3,5,'لون خلفية حقل النص');
--insert into linkElementWithProperty values (3,4,'لون خلفية حقل النص');
--insert into linkElementWithProperty values (4,6,'لون خلفية حقل النص');

--***************************************************************************************************************************************************
-- جدول ربط العناصر بالاحداث
create table linkElementWithEvent
(
linkElementWithEvent_elementNo int references element(element_no) on update cascade,
linkElementWithEvent_eeventNo int references eevent(eevent_no) on update cascade ,
linkElementWithEvent_description nvarchar(500),
primary key (linkElementWithEvent_elementNo,linkElementWithEvent_eeventNo)
)
go
--insert into linkElementWithEvent values (1,1,'شرح ححح');
--insert into linkElementWithEvent values (1,2,'شرح ححح');
--insert into linkElementWithEvent values (1,3,'شرح ححح');
--insert into linkElementWithEvent values (1,4,'شرح ححح');
--insert into linkElementWithEvent values (2,2,'شرح ححح');
--insert into linkElementWithEvent values (3,4,'شرح ححح');
--insert into linkElementWithEvent values (4,3,'شرح ححح');
--insert into linkElementWithEvent values (2,3,'شرح ححح');
--insert into linkElementWithEvent values (3,2,'شرح ححح');
--insert into linkElementWithEvent values (4,1,'شرح ححح');
--***************************************************************************************************************************************************
-- استعلام لعرض العناصر والخصائص
create view ElementWithproperty 
as
select element_no,element_name,element_description,property_no,property_name,property_description,linkElementWithProperty_description,propertyGroup_no,propertyGroup_name,propertyGroup_description
from element
inner join linkElementWithProperty on element.element_no=linkElementWithProperty.linkElementWithProperty_elementNo 
inner join property on property.property_no=linkElementWithProperty.linkElementWithProperty_propertyNo
inner join propertyGroup on property.property_propertyGroupNo=propertyGroup.propertyGroup_no
go

--select * from ElementWithproperty
--***************************************************************************************************************************************************
-- استعلام لعرض العناصر والاحداث
create view ElementWithEvent
as
select element_no,element_name,element_description,eevent_no,eevent_name,eevent_description,linkElementWithEvent_description,eventGroup_no,eventGroup_name,eventGroup_description
from element
inner join linkElementWithEvent on linkElementWithEvent.linkElementWithEvent_elementNo=element.element_no
inner join eevent on eevent.eevent_no=linkElementWithEvent.linkElementWithEvent_eeventNo
inner join eventGroup on eevent.eevent_eventGroupNo=eventGroup.eventGroup_no
go

--select * from ElementWithEvent

