DROP DATABASE IF EXISTS bd_final;
CREATE DATABASE bd_final;
USE bd_final;
CREATE TABLE JUGADOR (

 ID INT NOT NULL,

 NOMBRE VARCHAR(50),

 PASSWORD VARCHAR(50),

 USERNAME VARCHAR(50),

 PRIMARY KEY (ID) -- aqui sobraba una coma con lo cual SQL no compilaba y la tabla no se creaba

);ENGINE=InnoDB;



CREATE TABLE PARTIDA (

 ID INT NOT NULL,

 GANADOR VARCHAR(60),

 PRIMARY KEY (ID)

);ENGINE=InnoDB;




CREATE TABLE JUGADORPARTIDA (

 JUGADOR_ID INT NOT NULL,

 PARTIDA_ID INT NOT NULL,

 JUGADORES INT,

 FOREIGN KEY (JUGADOR_ID) REFERENCES JUGADOR(ID), -- como la tabla jugador no se habia creado por el error de la coma esto tambien petaba ya que la tabla jugador no existia

 FOREIGN KEY (PARTIDA_ID) REFERENCES PARTIDA(ID)

);











INSERT INTO JUGADOR VALUES (1, 'Monica', 'mesi123', 'monicagamer');

INSERT INTO JUGADOR VALUES (2, 'Mark', 'marco123', 'mark777');

INSERT INTO JUGADOR VALUES (3, 'Rachel', 'cr7siu', 'rachelthebest');



INSERT INTO PARTIDA VALUES (1, 'monicagamer');




