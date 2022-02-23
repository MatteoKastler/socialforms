create database web_4a_g1 collate utf8mb4_general_ci;

use web_4a_g1;

create table users(
	user_id int unsigned not null auto_increment,
    username varchar(100) not null,
    password varchar(300) not null,
    birthdate date null,
    email varchar(100) not null,
    gender int null,
    
    constraint user_id_PK primary key(user_id)
);
/*drop table useres;*/
insert into users value(null, "matteo", sha2("Hallo123!", 512), "2004-12-23", "m.t@gmx.at", 0);
insert into users value(null, "paula", sha2("Hallo123!", 512), "2004-08-12", "p@gmx.at", 1);
insert into users value(null, "timo", sha2("olsadflj334!", 512), "2004-01_01", "t@gmx.at", 0);

select * from users;