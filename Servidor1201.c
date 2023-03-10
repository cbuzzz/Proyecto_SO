#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <mysql.h>
#include <netinet/in.h>
#include <stdio.h>
#include <pthread.h>
//#include <my_global.h>
#define MAX 100

pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;
int puerto = 9060;
//
// Estructura para un usuario conectado al servidor.
//
typedef struct
{
	char nombre[20];
	int socket;
} Conectado;
//
// Estructura de lista de conectados.
//
typedef struct
{
	Conectado conectados[100];
	int num;
} ListaConectados;





char ID[3];
ListaConectados miLista;
char barcosJ1[100];
char jugadoresjugados[200];
//
//Funcion que pone en la lista de conectados un usuario
//Anade un nuevo conectado en la lista de conectados o devuelve un -1 si la lista esta llena
//
int Pon(ListaConectados* lista, char nombre[20], int socket)
{
	if (lista->num == 100)
	{
		return -1;
	}
	else
	{
		strcpy(lista->conectados[lista->num].nombre, nombre);
		lista->conectados[lista->num].socket = socket;
		lista->num++;
		return 0;
	}
}
//
//Devuelve el socket del conectado o un -1 si no lo encuentra
//
int DameSocket(ListaConectados* lista, char nombre[20])
{
	int i = 0;
	int encontrado = 0;
	while ((i < lista->num) && !encontrado)
	{
		if (strcmp(lista->conectados[i].nombre, nombre) == 0)
			encontrado = 1;
		if (!encontrado)
			i = i + 1;
	}
	if (encontrado)
		return lista->conectados[i].socket;
	else
		return -1;
}
//
//Funcion que devuelve la posicion de un usuario pasado por parametro.
//Devuelve la posicion de un usuario en la lista o un -1 si no lo encuentra
//
int DamePosicion(ListaConectados* lista, char nombre[20])
{
	int i = 0;
	int encontrado = 0;
	while ((i < lista->num) && (!encontrado))
	{
		if (strcmp(lista->conectados[i].nombre, nombre) == 0)
		{
			encontrado = 1;
		}
		if(!encontrado)
			i++;
	}
	if (encontrado)
	{
		return i;
		
	}
	else
	{
		return -1;
	}
}
//
// Funcion que elimina de la lista de conectados el usuario pasado como parametro.
// Devuelve un 0 si se elimina correctamente o un -1 en caso contrario.
//
int Elimina(ListaConectados* lista, char nombre[20])
{
	printf("%s:%d \n", lista->conectados[0].nombre, lista->num);
	printf("Nombre recibido como parametro: %s \n", nombre);
	
	int pos = DamePosicion(lista, nombre);
	
	if (pos == -1)
	{
		return -1;
	}
	else
	{
		int i;
		for (i = pos; i < lista->num - 1; i++)
		{
			lista->conectados[i] = lista->conectados[i + 1];
		}
		lista->num--;
		return 0;
	}
}
//
// Funcion que llena un vector de caracteres con la lista de conectados.
//
void DameConectados(ListaConectados* lista, char conectados[300])
{
	int i;
	for (i = 0; i < lista->num; i++)
	{
		sprintf(conectados, "%s%s/", conectados, lista->conectados[i].nombre);
	}
}



//
// Funcion para Loguearse.
// Devuelve un -1 si no se ha encontrado el usuario en la base de datos o un 0 si se logea correctamente
//
int Login(char respuesta[512], char username[20], char password[20], MYSQL* conn)
{
	char consulta[200];
	MYSQL_RES* resultado;
	MYSQL_ROW row;
	
	/*	strcpy(consulta, "SELECT JUGADOR.USERNAME,JUGADOR.PASSWORD FROM JUGADOR WHERE JUGADOR.USERNAME='");*/
	/*	strcat(consulta, username);*/
	/*	strcat(consulta, "' AND JUGADOR.PASSWORD='");*/
	/*	strcat(consulta, password);*/
	/*	strcat(consulta, "';\n");*/
	sprintf(consulta,"SELECT JUGADOR.USERNAME,JUGADOR.PASSWORD FROM JUGADOR WHERE JUGADOR.USERNAME='%s' AND JUGADOR.PASSWORD='%s';\n",username,password);
	
	int err = mysql_query(conn, consulta);
	if (err != 0)
	{
		printf("El USERNAME y el PASSWORD no coinciden %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit(1);
	}
	
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	
	if (row == NULL)
	{
		printf("El USERNAME y el PASSWORD no coinciden\n");
		strcpy(respuesta, "El usuario NO ha podido loguearse, revise si el usuario y la contrasena coinciden.");
		return -1;
	}
	
	else
		while (row != NULL)
	{
			printf("Bienvenido %s !\n", row[0]);
			row = mysql_fetch_row(resultado);
			return 0;
	}
}

//
// Funcion que retorna el ID del ?ltimo jugador registrado en la BBDD.
// Retorna un -1 en caso de que no haya ningun jugador registrado en la BBDD.
//
int DameIDJugador(MYSQL* conn)
{
	int err;
	MYSQL_RES* resultado;
	MYSQL_ROW row;
	
	char consulta[200];
	//int cont;
	
	strcpy(consulta, "SELECT JUGADOR.ID FROM JUGADOR ORDER BY JUGADOR.ID DESC LIMIT 1 ");
	err = mysql_query(conn, consulta);
	if (err != 0)
	{
		printf("Consulta mal hecha %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit(1);
	}
	
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	
	if (row == NULL)
	{
		printf("No hay jugadores registrados\n");
		return -1;
	}
	
	else
	{
		int cont;
		char numero[3];
		while (row != NULL)
		{
			printf("ID del ??ltimo jugador registrado: %s\n", row[0]);
			cont = atoi(row[0]);
			row = mysql_fetch_row(resultado);
		}
		return cont;
	}
}

int Registrar(char respuesta[200], char name[30], char username[20], char password[20], MYSQL* conn)
{
	
	int err;
	MYSQL_RES* resultado;
	MYSQL_ROW row;
	char consulta[200];
	
	sprintf(consulta, "SELECT * FROM JUGADOR WHERE JUGADOR.USERNAME='%s';",username);
	err = mysql_query(conn, consulta);
	if (err != 0)
	{
		printf("Consulta mal hecha %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit(1);
	}
	
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	
	if (row == NULL)
	{
		int ID = DameIDJugador(conn);
		int IDnum = ID + 1;
		sprintf(consulta,"INSERT INTO JUGADOR VALUES(%d,'%s','%s','%s');",IDnum,name,password,username);
		
		printf("consulta = %s\n", consulta);
		
		err = mysql_query(conn, consulta);
		//parte de mysql
		/*char user[20];*/
		
		if (err != 0)
		{
			printf("Error al crear la conexion: %u %s\n", mysql_errno(conn), mysql_error(conn));
			return 1;
			exit(1);
		}
		
		return 0;
	}
	else
		return -1;
}

int DarDeBaja(char respuesta[200], char username[20], char password[20], MYSQL* conn)
{
	
	int err;
	MYSQL_RES* resultado;
	MYSQL_ROW row;
	char consulta[200];
	
	sprintf(consulta, "SELECT * FROM JUGADOR WHERE JUGADOR.USERNAME='%s' AND JUGADOR.PASSWORD='%s';",username,password);
	err = mysql_query(conn, consulta);
	if (err != 0)
	{
		printf("Consulta mal hecha %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit(1);
	}
	
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	
	if (row != NULL)
	{
		sprintf(consulta,"DELETE FROM JUGADOR WHERE JUGADOR.USERNAME='%s' AND JUGADOR.PASSWORD='%s';",username,password);
		
		printf("consulta = %s\n", consulta);
		
		err = mysql_query(conn, consulta);
		//parte de mysql
		/*char user[20];*/
		
		if (err != 0)
		{
			printf("Error al crear la conexion: %u %s\n", mysql_errno(conn), mysql_error(conn));
			return 1;
			exit(1);
		}
		
		return 0;
	}
	else
		return -1;
}
int DameUsuariosJugados (char username[20], MYSQL *conn, char jugadores[200])
{
	char consulta[500];
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int cont;
	
	strcpy (consulta, "SELECT DISTINCT JUGADOR.USERNAME FROM (JUGADOR, PARTIDA, JUGADORPARTIDA) WHERE PARTIDA.ID IN ");
	strcat (consulta, "( SELECT PARTIDA.ID FROM (JUGADOR,PARTIDA,JUGADORPARTIDA) "); 
	strcat (consulta, "WHERE JUGADOR.USERNAME = '");
	strcat (consulta, username); 
	strcat (consulta, "' ");
	strcat (consulta,"AND JUGADOR.ID = JUGADORPARTIDA.JUGADOR_ID ");
	strcat (consulta,"AND JUGADORPARTIDA.PARTIDA_ID = PARTIDA.ID) ");
	strcat (consulta,"AND PARTIDA.ID = JUGADORPARTIDA.PARTIDA_ID ");
	strcat (consulta,"AND JUGADORPARTIDA.JUGADOR_ID = JUGADOR.ID ");
	strcat (consulta, "AND JUGADOR.USERNAME NOT IN ('");
	strcat (consulta, username); 
	strcat (consulta, "');");
	
	
	int err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("Consulta mal hecha %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	if (row == NULL)
	{
		printf ("No has jugado ninguna partida\n");
		cont = 0;
		return -1;
	}
	
	else 
	{
		cont = 0; 
		
		while (row !=NULL) 
		{
			printf ("Has jugado contra: %s\n", row[0]);
			sprintf(jugadoresjugados, "%s%s/", jugadoresjugados, row[0]);
			row = mysql_fetch_row (resultado);
			cont= cont +1;
		}
		strcpy(jugadores,jugadoresjugados);
		printf("Jugadores con los que has jugado alguna partida : %s\n",jugadores);
		return 0;
	}
}
//
// Funcion que pone en el vector "ganadores"(pasado como parametro) los ganadores de todas las  
// partidas jugadas contra un determinado jugador("contrincante").
// Retorna el numero de partidos contra ese jugador o un -1 en caso de que no hayan jugado ninguna partida.
//
int DameResultados (char username[20], char contrincante[20], MYSQL *conn, char ganadores[200])
{
	char consulta[500];
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int cont;
	
	strcpy (consulta, "SELECT PARTIDA.GANADOR FROM (JUGADOR,PARTIDA,JUGADORPARTIDA) WHERE PARTIDA.ID IN ");
	strcat (consulta, "( SELECT PARTIDA.ID FROM (JUGADOR,PARTIDA, JUGADORPARTIDA) "); 
	strcat (consulta, "WHERE JUGADOR.USERNAME = '");
	strcat (consulta, username); 
	strcat (consulta, "' ");
	strcat (consulta,"AND JUGADOR.ID = JUGADORPARTIDA.JUGADOR_ID ");
	strcat (consulta,"AND JUGADORPARTIDA.ID_P = PARTIDA.ID) ");
	strcat (consulta,"AND PARTIDA.ID = JUGADORPARTIDA.PARTIDA_ID ");
	strcat (consulta,"AND JUGADORPARTIDA.JUGADOR_ID = JUGADOR.ID ");
	strcat (consulta,"AND JUGADOR.USERNAME = '");
	strcat (consulta, contrincante); 
	strcat (consulta, "' ");
	strcat (consulta,"AND JUGADOR.ID = JUGADORPARTIDA.JUGADOR_ID ");
	strcat (consulta,"AND JUGADORPARTIDA.PARTIDA_ID = PARTIDA.ID; ");
	
	int err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("Consulta mal hecha %u %s\n", mysql_errno(conn), mysql_error(conn));
		cont = 0;
		exit (1);
	}
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	if (row == NULL)
	{
		printf ("El jugador no ha jugado niguna partida\n");
		return -1;
	}
	
	else 
	{
		cont = 0; 
		char losganadores[200];
		strcpy(losganadores, "");
		while (row !=NULL) 
		{
			printf ("Ganador de la partida: %s\n", row[0]);
			sprintf(losganadores, "%s%s/", losganadores, row[0]);
			row = mysql_fetch_row (resultado);
			cont= cont +1;
		}
		strcpy(ganadores,losganadores);
		printf("EN LA FUNCION SALE :  %s\n", losganadores);
		return cont;
	}
}
int DameTodosUsuarios ( MYSQL *conn, char respuesta[200], char username[200])
{
	char consulta[500];
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int cont;
	
	strcpy (consulta, "SELECT * FROM JUGADOR WHERE JUGADOR.USERNAME NOT IN ('");
	strcat (consulta, username); 
	strcat (consulta, "');");
	
	int err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("Consulta mal hecha %u %s\n", mysql_errno(conn), mysql_error(conn));
		cont = 0;
		exit (1);
	}
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	if (row == NULL)
	{
		printf ("No hay jugadores registrados\n");
		return -1;
	}
	
	else 
	{
		cont = 0; 
		strcpy(respuesta,"");
		while (row !=NULL) 
		{
			printf ("Username:     %s\n", row[1]);
			sprintf(respuesta, "%s%s/", respuesta, row[1]);
			row = mysql_fetch_row (resultado);
			cont= cont +1;
		}
		return cont;
	}
}
//
// Funcion que retorna el numero de partidas ganadas del usuario pasado como par???metro.
// Retorna un -1 en caso de no haber encontrado ning n jugador con ese nombre de usuario.
//
int DamePartidasGanadas (char username[20], MYSQL *conn)
{
	char consulta[500];
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int cont;
	
	strcpy (consulta, "SELECT JUGADOR.USERNAME FROM (JUGADOR,PARTIDA,JUGADORPARTIDA) ");
	strcat (consulta, "WHERE JUGADOR.USERNAME = '");
	strcat (consulta, username); 
	strcat (consulta, "' ");
	strcat (consulta,"AND JUGADOR.ID = JUGADORPARTIDA.JUGADOR_ID ");
	strcat (consulta,"AND JUGADOR.PARTIDA_ID= PARTIDA.ID ");
	strcat (consulta,"AND PARTIDA.GANADOR ='");
	strcat (consulta, username); 
	strcat (consulta, "';");
	
	
	int err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("Consulta mal hecha %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	if (row == NULL)
	{
		printf ("No has ganado ninguna partida\n");
		cont = 0;
		return -1;
	}
	
	else 
	{
		cont = 0; 
		char partidasganadas[200];
		strcpy(partidasganadas,"");
		while (row !=NULL) 
		{
			printf ("PARTIDA GANADA: %s\n", row[0]);
			sprintf(partidasganadas, "%s%s/", partidasganadas, row[0]);
			row = mysql_fetch_row (resultado);
			cont= cont +1;
		}
		printf("Numero de partidas ganadas : %d\n",cont);
		return cont;
	}
}
//
//   A T E N D E R   C L I E N T E
//
void* AtenderCliente(void* socket)
{
	int* s;
	s = (int*)socket;
	int sock_conn = *(int*)socket;
	sock_conn = *s;
	
	int i = miLista.num;
	miLista.conectados[i].socket = *s;
	
	
	char peticion[512];
	char respuesta[512];
	char respuesta2[512];
	int ret;
	MYSQL* conn;
	int err;
	MYSQL_RES* resultado;
	MYSQL_ROW row;
	int numID = 6;
	char consulta[80];
	char notificacion[200];
	char connected[200];
	char conectados[200];
	char invitador[30];
	char invitado[30];
	
	//Conexion con la base de datos
	conn = mysql_init(NULL);
	if (conn == NULL)
	{
		printf("Error al crear la conexion: %u %s\n",
			   mysql_errno(conn), mysql_error(conn));
		exit(1);
	}
	
	conn = mysql_real_connect(conn, "localhost", "root", "mysql", "bd_final", 0, NULL, 0);
	if (conn == NULL)
	{
		printf("Error al inicializar la conexion: %u %s\n",mysql_errno(conn), mysql_error(conn));
		exit(1);
	}
	//Empieza a escuchar las peticiones del cliente
	int terminar = 0;
	while (terminar == 0)
	{
		ret = read(sock_conn, peticion, sizeof(peticion));
		printf("Recibido\n");
		peticion[ret] = '\0';
		printf("Peticion: %s\n", peticion);
		
		char* p = strtok(peticion, "/");
		int codigo = atoi(p);
		char username[50];
		char password[50];
		char name[50];
		char conectados[200];
		//
		// Peticion de DESCONEXION.
		//
		if (codigo == 0)
		{
			p = strtok(NULL, "/");
			strcpy(username, p);
			pthread_mutex_lock(&mutex);
			int res = Elimina(&miLista, username);
			pthread_mutex_unlock(&mutex);
			if (res == 0){
				strcpy(respuesta,"0/Si");
				write(sock_conn, respuesta, strlen(respuesta));
				printf("Usuario eliminado de la lista de conectados\n");
				strcpy(conectados,"3/");
				
				DameConectados(&miLista,conectados);
				
				printf("Usuarios conectados: %s\n",conectados);
				for(i=0;i<miLista.num;i++){
					write(miLista.conectados[i].socket, conectados, strlen(conectados));
				}
				terminar = 1;
			}
			else{
				strcpy(respuesta,"0/No");
				printf("Error al eliminar el usuario de la lista de conectados\n");
				write(sock_conn, respuesta, strlen(respuesta));
			}
			
		}
		//
		// Peticion de LOGUEAR.
		//
		else if (codigo == 1)
		{
			p = strtok(NULL, "/");
			strcpy(username, p);
			
			p = strtok(NULL, "/");
			strcpy(password, p);
			
			int res = Login(respuesta, username, password, conn);
			
			if (res == 0)
			{
				if (res == 0){
					strcpy(respuesta,"1/Si");
					printf("Anadido a la lista de conectados\n");
					
					pthread_mutex_lock(&mutex);
					int res =Pon(&miLista,username,sock_conn);
					pthread_mutex_unlock(&mutex);
					strcpy(conectados,"3/");
					
					DameConectados(&miLista,conectados);
					
					printf("Usuarios conectados: %s\n",conectados);
					for(i=0;i<miLista.num;i++){
						
						write(miLista.conectados[i].socket, conectados, strlen(conectados));
						
					}
					printf("%s\n", username);
					
				}
				
				else {
					printf("Lista llena. No se pudo anadir el usuario a la lista de conectados.\n");
					strcpy(respuesta,"1/No");
				}
				
				write(sock_conn, respuesta, strlen(respuesta));
				
				
				
				
			}
			else
			{
				printf("El usuario NO ha podido loguearse, revise si el usuario y la contrasena coinciden.");
				strcpy(respuesta, "1/No");
				
				write(sock_conn, respuesta, strlen(respuesta));
				
			}
		}
		//
		// Peticion de REGISTRAR.
		//
		else if (codigo == 2)
		{
			p = strtok(NULL, "/");
			strcpy(name, p);
			
			p = strtok(NULL, "/");
			strcpy(username, p);
			
			p = strtok(NULL, "/");
			strcpy(password, p);
			
			int res = Registrar(respuesta, name, username, password, conn);
			if(res == 0){
				strcpy(respuesta,"2/Si");
				
				write(sock_conn,respuesta,strlen(respuesta));
				
			}
			else if(res==1){
				strcpy(respuesta,"2/No");
				
				write(sock_conn,respuesta,strlen(respuesta));
				
			}
			else{
				strcpy(respuesta,"2/F");
				
				write(sock_conn,respuesta,strlen(respuesta));
				
			}
		}
		//
		// Peticion de INVITAR.
		//
		else if (codigo == 4)
		{
			char source[30];
			char target[30];
			p = strtok(NULL, "/");
			strcpy(target, p);
			p = strtok(NULL, "/");
			strcpy(source, p);
			pthread_mutex_lock(&mutex);
			int res = DameSocket(&miLista, target);
			pthread_mutex_unlock(&mutex);
			if(res == -1){
				strcpy(respuesta,"4/No");
				
				write(sock_conn,respuesta,strlen(respuesta));
				
			}
			else {
				sprintf(respuesta,"4/Si/%s",source);
				
				write(res,respuesta,strlen(respuesta));
				
			}
		}
		//
		// Peticion mensaje Chat
		//
		else if (codigo == 5)
		{
			p = strtok(NULL, "/");
			strcpy(username, p);
			char mensaje[200];
			p = strtok(NULL, "/");
			strcpy(mensaje, p);
			sprintf(respuesta,"5/%s/%s",username,mensaje);
			for(i=0;i<miLista.num;i++)
				write(miLista.conectados[i].socket, respuesta, strlen(respuesta));
			
		}
		//
		// Petici?n dar de baja a un usuario
		//
		else if (codigo == 6)
		{
			p = strtok(NULL, "/");
			strcpy(username, p);
			
			p = strtok(NULL, "/");
			strcpy(password, p);
			
			int res = DarDeBaja(respuesta, username, password, conn);
			if(res == 0){
				strcpy(respuesta,"6/Si");
				
				write(sock_conn,respuesta,strlen(respuesta));
				
			}
			else if(res==-1){
				strcpy(respuesta,"6/No");
				
				write(sock_conn,respuesta,strlen(respuesta));
				
			}
			else{
				strcpy(respuesta,"6/F");
				
				write(sock_conn,respuesta,strlen(respuesta));
				
			}
		}
		//
		// Usuario acepta o rechaza invitaci?n
		//
		else if (codigo == 7)
		{
			p = strtok(NULL, "/");
			
			if(strcmp(p,"Si") == 0){
				int turno1 = 1;
				int turno2 = 0;
				p = strtok(NULL, "/");
				strcpy(invitado, p);
				p = strtok(NULL, "/");
				strcpy(invitador, p);
				sprintf(respuesta,"7/Si/%d",turno1);
				sprintf(respuesta2,"7/Si/%d",turno2);
				printf("Respuesta: %s\n",respuesta);
				for(i=0;i<miLista.num;i++){
					if(strcmp(miLista.conectados[i].nombre,invitado) == 0)
						write(miLista.conectados[i].socket, respuesta, strlen(respuesta));
					else if(strcmp(miLista.conectados[i].nombre,invitador) == 0)
						write(miLista.conectados[i].socket, respuesta2, strlen(respuesta));
				}
			}
			else{
				
				p = strtok(NULL, "/");
				strcpy(invitado, p);
				p = strtok(NULL, "/");
				strcpy(invitador, p);
				for(i=0;i<miLista.num;i++)
				{
					if(strcmp(miLista.conectados[i].nombre,invitado) == 0){
						sprintf(respuesta,"7/Rechazado");
						printf("Respuesta: 7/Rechazado\n");
						write(miLista.conectados[i].socket, respuesta, strlen(respuesta));
					}
					
					else if(strcmp(miLista.conectados[i].nombre,invitador)==0){
						sprintf(respuesta,"7/F");
						printf("Respuesta: 7/F\n");
						write(miLista.conectados[i].socket, respuesta, strlen(respuesta));
					}
				}
			}
			
		}
		//
		// Jugador env?a lista de barcos escogidos
		//
		else if (codigo == 8)
		{
			strcpy(barcosJ1,"");
			p = strtok(NULL, "/");
			int nForm = atoi(p);
			p = strtok(NULL, "/");
			char j2[30];
			strcpy(j2,p);
			int i=0;
			for(i=0;i<17;i++){
				p = strtok(NULL, "/");
				sprintf(barcosJ1,"%s%s/",barcosJ1,p);
			}
			sprintf(respuesta,"8/%d/%s",nForm,barcosJ1);
			printf("Respuesta: %s\n",respuesta);
			for(i=0;i<miLista.num;i++){
				if(strcmp(miLista.conectados[i].nombre,j2) == 0)
					write(miLista.conectados[i].socket, respuesta, strlen(respuesta));
			}
			
		}
		//
		// Jugador casilla que ha atacado
		//
		else if (codigo == 9)
		{
			
			p = strtok(NULL, "/");
			int nForm = atoi(p);
			p = strtok(NULL, "/");
			char casilla[3];
			strcpy(casilla,p);
			p = strtok(NULL, "/");
			char j2[30];
			strcpy(j2,p);
			sprintf(respuesta,"9/%d/%s",nForm,casilla);
			printf("Respuesta: %s\n",respuesta);
			for(i=0;i<miLista.num;i++){
				if(strcmp(miLista.conectados[i].nombre,j2) == 0)
					write(miLista.conectados[i].socket, respuesta, strlen(respuesta));
			}
			
		}
		else if(codigo == 10)
		{
			char JugadoresJugados[200];
			char jugadores[200];
			int ans = DameUsuariosJugados(username,conn,JugadoresJugados);
			if (ans == 0)
			{
				sprintf(jugadores,"10/%s",JugadoresJugados);
				printf("%s\n", jugadores);
				write(sock_conn, jugadores , strlen(jugadores));
			}
			else
			{
				strcpy(respuesta,"10/No");
				write(sock_conn, respuesta, strlen(respuesta));
			}
		}
		else if(codigo == 11)
		{
			char Usuarios[200];
			int resultado = DameTodosUsuarios(conn, Usuarios,username);
			
			if (resultado != -1)
			{
				sprintf(respuesta,"11-%d/%s", resultado, Usuarios);
				printf("%s\n", respuesta);
				write(sock_conn, respuesta , strlen(respuesta));
			}
			else
			{
				sprintf(respuesta,"11-%s", respuesta);
				write(sock_conn, respuesta, strlen(respuesta));
			}
		}
		else if(codigo == 12)
		{
			char ganadoress[200];
			char ganadorescliente[200];
			char contrincante[20]; //Jugador del que quiero ver resultados de las partidas jugadas conta mi.
			
			p = strtok( NULL, "/");
			strcpy(contrincante,p);
			
			int cont = DameResultados(username,contrincante,conn,ganadoress);
			if (cont != -1)
			{
				sprintf(ganadorescliente,"12-%d/%s", cont, ganadoress);
				printf("Esto lo enviamos al cliente %s\n", ganadorescliente);
				write(sock_conn, ganadorescliente , strlen(ganadorescliente));
			}
			else
			{
				sprintf(respuesta,"12-0/%s", respuesta);
				write(sock_conn, respuesta, strlen(respuesta));
			}
		}
		else if(codigo == 13)
		{
			char jugador[20]; // jugador del que quiero ver el numero las partidas ganadas.
			strcpy(jugador,"");
			
			p = strtok( NULL, "/");
			strcpy(jugador,p);
			
			int PartidasGanadas = DamePartidasGanadas(jugador,conn);
			sprintf(respuesta,"13-%d",PartidasGanadas);
			write(sock_conn, respuesta , strlen(respuesta));
		}
	}
	close(sock_conn);
	
}
//
// Aqui empieza el MAIN.
//
int main(int argc, char* argv[])
{
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	
	memset(&serv_adr, 0, sizeof(serv_adr));
	serv_adr.sin_family = AF_INET;
	
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	
	serv_adr.sin_port = htons(puerto);
	if (bind(sock_listen, (struct sockaddr*)&serv_adr, sizeof(serv_adr)) < 0)
		printf("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	int i;
	int sockets[100];
	
	pthread_t thread[10];
	ListaConectados miLista;
	i = 0;
	
	for (;;)
	{
		printf("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf("He recibido conexion\n");
		
		sockets[i] = sock_conn;
		miLista.conectados[i].socket = sock_conn;
		miLista.num = i;
		
		pthread_create(&thread[i], NULL, AtenderCliente, &sockets[i]);
		i++;
	}
}



