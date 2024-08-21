create database db_tecnoferre

use db_tecnoferre

create table rol(
id int primary key,
tipo varchar(20)
);


create table usuario(
cedula int primary key,
nombre varchar(100) not null,
correo varchar(100) unique  not null,
contrasena varchar(100)  not null,
direccion varchar(200),
rol int foreign key references Rol(id)
)

create table producto(
id int primary key identity (1,1),
nombre varchar(100) not null,
categoria varchar(100) not null,
precio decimal(8, 2) not null,
descripcion varchar(500),
imagen varchar(250),
fecha_ingreso date  not null
)

CREATE TABLE Cart (
    CartId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT FOREIGN KEY REFERENCES Usuario(cedula), 
    CreatedDate DATE NOT NULL DEFAULT�GETDATE()
);

CREATE TABLE CartItems (
    CartItemId INT PRIMARY KEY IDENTITY(1,1),
    CartId INT FOREIGN KEY REFERENCES Cart(CartId) ON DELETE CASCADE, 
    ProductId INT FOREIGN KEY REFERENCES Producto(id), 
    Quantity INT NOT NULL,
    Price DECIMAL(8,2)�NOT�NULL
);

CREATE TABLE Orders (
    OrderId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT FOREIGN KEY REFERENCES Usuario(cedula),
    OrderDate DATE NOT NULL DEFAULT GETDATE(),
    TotalAmount DECIMAL(10, 2) NOT NULL,
    PaymentStatus VARCHAR(50)�NOT�NULL
);

create table factura(
id int primary key identity (1,1),
nFactura varchar(50),
id_usuario int foreign key references Usuario(cedula),
fechaEmision datetime,
subtotal decimal(18,2),
total DECIMAL(18,2) not null, 
id_carrito  int foreign key references Cart(CartId), 
id_mensajero int foreign key references Usuario(cedula),
Estado VARCHAR(20) DEFAULT 'Pendiente'
);


create table facturaItems(
id int primary key identity (1,1),
FacturaId  INT FOREIGN KEY REFERENCES Factura(id),
ProductoId INT FOREIGN KEY REFERENCES Producto(id), 
Quantity INT NOT NULL,
Price DECIMAL(8,2)�NOT�NULL
);


insert into rol(id, tipo) values(1, 'Admin');
insert into rol(id, tipo) values(2, 'Cliente');
insert into rol(id, tipo) values(3, 'Mensajero');


insert into usuario (cedula, nombre, correo, contrasena, rol) values (1, 'admin','admin@gmail.com', 'tecnoAdmin12', 1)
insert into usuario (cedula, nombre, correo, contrasena, direccion, rol) values (111089234, 'Daniel Mora','daniel.mora@gmail.com', 'Mensajero12', ' ' , 3)

insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Termoducha', 'Ba�o', 20000, 'Combina funcionalidad y dise�o moderno para elevar tu experiencia en el ba�o. Equipado con tecnolog�a avanzada para un control preciso de la temperatura del agua, garantiza una ducha c�moda y agradable cada vez. Su construcci�n robusta y materiales de alta calidad aseguran durabilidad y resistencia al desgaste. F�cil de instalar y compatible con la mayor�a de los sistemas de agua.', 'https://i.imghippo.com/files/bh6Jk1722812930.jpg', '2024-07-01')
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Rodillo', 'Pintura y Accesorios', 7000, 'Una herramienta esencial para lograr acabados de pintura uniformes y profesionales en superficies amplias. Su dise�o eficiente facilita la aplicaci�n de pintura, ahorrando tiempo y esfuerzo en proyectos grandes. Fabricado con materiales duraderos, ofrece resistencia y una larga vida �til, mientras que su mango ergon�mico asegura un agarre c�modo, incluso durante sesiones prolongadas.', 'https://i.imghippo.com/files/u5rHn1722814543.jpg', '2024-07-01');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Cinta M�trica', 'Medici�n', 2000, 'Una herramienta imprescindible para todo tipo de trabajos de medici�n. Con una longitud adecuada para una variedad de proyectos, su dise�o compacto y robusto permite un uso pr�ctico y preciso. Fabricada con materiales resistentes, asegura durabilidad y una medici�n exacta, siendo ideal tanto para tareas dom�sticas como profesionales.', 'https://i.imghippo.com/files/IKkyg1722814341.jpg', '2024-07-01');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Llave Inglesa', 'Herramientas', 20000, 'Una herramienta esencial en cualquier caja de herramientas. Su dise�o ajustable permite adaptarse a diferentes tama�os de tuercas y tornillos, ofreciendo versatilidad en una variedad de aplicaciones. Fabricada con acero de alta calidad, proporciona resistencia y durabilidad, soportando el uso intensivo sin deformarse. Su mango ergon�mico asegura un agarre c�modo y firme, facilitando el trabajo en espacios reducidos.', 'https://i.imghippo.com/files/NMhnP1722814401.jpg', '2024-07-01');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Martillo', 'Herramientas', 15000, 'Una herramienta fundamental para cualquier proyecto de construcci�n o reparaci�n. Su dise�o robusto y equilibrado permite una fuerza de impacto eficaz, ideal para clavar clavos y realizar tareas de demolici�n. Fabricado con materiales de alta calidad, garantiza durabilidad y resistencia al desgaste. El mango ergon�mico proporciona un agarre c�modo, reduciendo la fatiga durante el uso prolongado', 'https://i.imghippo.com/files/V8nPX1722814450.jpg', '2024-07-01');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Destornillador', 'Herramientas', 8000, 'Una herramienta vers�til y esencial para cualquier caja de herramientas. Su dise�o ergon�mico y mango antideslizante garantizan un agarre c�modo y firme, facilitando el manejo y reduciendo la fatiga. Fabricado con acero de alta calidad, ofrece durabilidad y precisi�n en el ajuste de tornillos.', 'https://i.imghippo.com/files/5GwGS1722814483.jpg', '2024-07-01');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Taladro', 'Herramientas El�ctricas', 120000, 'Una herramienta el�ctrica fundamental para perforar y realizar tareas de instalaci�n. Con un potente motor y m�ltiples velocidades, ofrece versatilidad y eficiencia en una variedad de materiales, desde madera hasta metal. Su dise�o ergon�mico y mango antideslizante aseguran un manejo c�modo y preciso, mientras que su construcci�n robusta garantiza durabilidad y resistencia. ', 'https://i.imghippo.com/files/gaOvw1722814514.jpg','2024-07-01');
 
 select * from usuario
