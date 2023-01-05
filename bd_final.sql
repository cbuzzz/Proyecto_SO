DROP DATABASE IF EXISTS bd_final;
CREATE DATABASE bd_final;
USE bd_final;
CREATE TABLE JUGADOR (

 ID INT NOT NULL,

 NOMBRE VARCHAR(50),

 PASSWORD VARCHAR(50),

 USERNAME VARCHAR(50),

 PRIMARY KEY (ID) -- aqui sobraba una coma con lo cual SQL no compilaba y la tabla no se creaba



);



CREATE TABLE PARTIDA (

 ID INT NOT NULL,

 FECHAFIN DATE,

 DURACION INT NULL,

 GANADOR VARCHAR(50),

 PRIMARY KEY (ID)

);



CREATE TABLE JUGADORPARTIDA (

 JUGADOR_ID INT NOT NULL,

 PARTIDA_ID INT NOT NULL,

 JUGADORES INT,

 FOREIGN KEY (JUGADOR_ID) REFERENCES JUGADOR(ID), -- como la tabla jugador no se habia creado por el error de la coma esto tambien petaba ya que la tabla jugador no existia

 FOREIGN KEY (PARTIDA_ID) REFERENCES PARTIDA(ID)

);



/* DATOS INSERTADOS EN PARTIDA / CARTAS / EQUIPOS HECHO POR MIS

COMPAÑEROS*/







INSERT INTO JUGADOR VALUES (1, 'Monica', 'mesi123', 'monicagamer', 1);

INSERT INTO JUGADOR VALUES (2, 'Mark', 'marco123', 'mark777', 2);

INSERT INTO JUGADOR VALUES (3, 'Rachel', 'cr7siu', 'rachelthebest', 1);




