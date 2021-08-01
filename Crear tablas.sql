CREATE Table Alumnos(
    Id INT NOT NULL, 
    Nombres varchar(50) NOT NULL,
    ApPaterno varchar(50) NOT NULL,
    Ap_Materno varchar(50) NOT NULL,
    Semestre int NOT NULL,
    PRIMARY KEY (Id)
)

CREATE TABLE DatosAcceso(
    Matricula int NOT NULL,
    Contraseña varchar(20) NOT NULL, 
    PRIMARY KEY (Matricula),
    FOREIGN KEY(Matricula)REFERENCES Alumnos(Id)
)

CREATE TABLE Pagos(
    Id_Pago int NOT NULL,
    Descripcion varchar(100) NOT NULL,
    Cantidad int NOT NULL,
    Fecha smalldatetime NOT NULL,
    Matricula int NOT NULL,
    Semestre int NOT NULL    
    PRIMARY KEY (Id_Pago),
    FOREIGN KEY (Matricula)REFERENCES DatosAcceso(Matricula),
)