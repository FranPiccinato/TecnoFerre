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
    CreatedDate DATE NOT NULL DEFAULT GETDATE()
);

CREATE TABLE CartItems (
    CartItemId INT PRIMARY KEY IDENTITY(1,1),
    CartId INT FOREIGN KEY REFERENCES Cart(CartId) ON DELETE CASCADE, 
    ProductId INT FOREIGN KEY REFERENCES Producto(id), 
    Quantity INT NOT NULL,
    Price DECIMAL(8,2) NOT NULL
);

CREATE TABLE Orders (
    OrderId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT FOREIGN KEY REFERENCES Usuario(cedula),
    OrderDate DATE NOT NULL DEFAULT GETDATE(),
    TotalAmount DECIMAL(10, 2) NOT NULL,
    PaymentStatus VARCHAR(50) NOT NULL
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
Price DECIMAL(8,2) NOT NULL
);


insert into rol(id, tipo) values(1, 'Admin');
insert into rol(id, tipo) values(2, 'Cliente');
insert into rol(id, tipo) values(3, 'Mensajero');


insert into usuario (cedula, nombre, correo, contrasena, direccion, rol) values (1, 'admin','admin@gmail.com', 'tecnoAdmin12', ' ' , 1)
insert into usuario (cedula, nombre, correo, contrasena, direccion, rol) values (111089234, 'Daniel Mora','daniel.mora@gmail.com', 'Mensajero12', ' ' , 3)

insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Termoducha', 'Baño', 20000, 'Combina funcionalidad y diseño moderno para elevar tu experiencia en el baño. Equipado con tecnología avanzada para un control preciso de la temperatura del agua, garantiza una ducha cómoda y agradable cada vez. Su construcción robusta y materiales de alta calidad aseguran durabilidad y resistencia al desgaste. Fácil de instalar y compatible con la mayoría de los sistemas de agua.', 'https://i.imghippo.com/files/bh6Jk1722812930.jpg', '2024-07-01')
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Rodillo', 'Pintura y Accesorios', 7000, 'Una herramienta esencial para lograr acabados de pintura uniformes y profesionales en superficies amplias. Su diseño eficiente facilita la aplicación de pintura, ahorrando tiempo y esfuerzo en proyectos grandes. Fabricado con materiales duraderos, ofrece resistencia y una larga vida útil, mientras que su mango ergonómico asegura un agarre cómodo, incluso durante sesiones prolongadas.', 'https://i.imghippo.com/files/u5rHn1722814543.jpg', '2024-07-01');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Cinta Métrica', 'Medición', 2000, 'Una herramienta imprescindible para todo tipo de trabajos de medición. Con una longitud adecuada para una variedad de proyectos, su diseño compacto y robusto permite un uso práctico y preciso. Fabricada con materiales resistentes, asegura durabilidad y una medición exacta, siendo ideal tanto para tareas domésticas como profesionales.', 'https://i.imghippo.com/files/IKkyg1722814341.jpg', '2024-07-01');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Llave Inglesa', 'Herramientas', 20000, 'Una herramienta esencial en cualquier caja de herramientas. Su diseño ajustable permite adaptarse a diferentes tamaños de tuercas y tornillos, ofreciendo versatilidad en una variedad de aplicaciones. Fabricada con acero de alta calidad, proporciona resistencia y durabilidad, soportando el uso intensivo sin deformarse. Su mango ergonómico asegura un agarre cómodo y firme, facilitando el trabajo en espacios reducidos.', 'https://i.imghippo.com/files/NMhnP1722814401.jpg', '2024-07-01');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Martillo', 'Herramientas', 15000, 'Una herramienta fundamental para cualquier proyecto de construcción o reparación. Su diseño robusto y equilibrado permite una fuerza de impacto eficaz, ideal para clavar clavos y realizar tareas de demolición. Fabricado con materiales de alta calidad, garantiza durabilidad y resistencia al desgaste. El mango ergonómico proporciona un agarre cómodo, reduciendo la fatiga durante el uso prolongado', 'https://i.imghippo.com/files/V8nPX1722814450.jpg', '2024-07-01');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Destornillador', 'Herramientas', 8000, 'Una herramienta versátil y esencial para cualquier caja de herramientas. Su diseño ergonómico y mango antideslizante garantizan un agarre cómodo y firme, facilitando el manejo y reduciendo la fatiga. Fabricado con acero de alta calidad, ofrece durabilidad y precisión en el ajuste de tornillos.', 'https://i.imghippo.com/files/5GwGS1722814483.jpg', '2024-07-01');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Taladro', 'Herramientas Eléctricas', 120000, 'Una herramienta eléctrica fundamental para perforar y realizar tareas de instalación. Con un potente motor y múltiples velocidades, ofrece versatilidad y eficiencia en una variedad de materiales, desde madera hasta metal. Su diseño ergonómico y mango antideslizante aseguran un manejo cómodo y preciso, mientras que su construcción robusta garantiza durabilidad y resistencia. ', 'https://i.imghippo.com/files/gaOvw1722814514.jpg','2024-07-01');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Cinta Adhesiva', 'Accesorios', 3000, 'Cinta adhesiva multiuso de alta resistencia, perfecta para una variedad de tareas, incluyendo empaques, reparaciones rápidas y sujeción temporal de objetos. Su adhesivo duradero garantiza una unión firme y segura.', 'https://i.imghippo.com/files/IecZ11724369995.webp', '2024-08-15');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Llave de Cruz', 'Herramientas', 5000, 'Llave de cruz para tuercas de vehículos, fabricada en acero de alta calidad para ofrecer durabilidad y resistencia. Ideal para trabajos mecánicos, especialmente en el cambio de llantas.', 'https://i.imghippo.com/files/gUj2L1724369975.webp', '2024-08-15');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Flexómetro', 'Medición', 2500, 'Flexómetro compacto de 5 metros, diseñado para ser resistente y fácil de transportar. Perfecto para medir con precisión en proyectos de construcción y bricolaje.', 'https://i.imghippo.com/files/z49Xj1724369947.webp', '2024-08-15');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Destornillador Plano', 'Herramientas', 4000, 'Destornillador plano con mango ergonómico, diseñado para proporcionar un agarre cómodo y firme. La punta de acero templado asegura una durabilidad excepcional durante el uso intensivo.', 'https://i.imghippo.com/files/SEUlE1724369919.jpg', '2024-08-15');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Llave de Tubo', 'Herramientas', 6000, 'Llave de tubo ajustable, ideal para trabajos de plomería. Fabricada en acero de alta resistencia, esta herramienta es perfecta para ajustar y aflojar tuercas y tornillos en espacios reducidos.', 'https://i.imghippo.com/files/J4mxi1724369897.webp', '2024-08-15');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Martillo de Goma', 'Herramientas', 8000, 'Martillo de goma con cabeza resistente, ideal para trabajos de carpintería y ensamblaje sin dañar las superficies. Su diseño equilibrado facilita su manejo y proporciona un impacto controlado.', 'https://i.imghippo.com/files/wxQMw1724369875.jpg', '2024-08-15');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Juego de Llaves Hexagonales', 'Herramientas', 7000, 'Set de llaves hexagonales en diferentes tamaños, fabricadas en acero resistente. Ideal para ensamblar muebles y ajustar tornillos con cabezas hexagonales en proyectos de bricolaje.', 'https://i.imghippo.com/files/dg0ya1724369849.jpg', '2024-08-15');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Serrucho', 'Herramientas', 9000, 'Serrucho de mano con dientes endurecidos, diseñado para cortar madera de manera eficiente y precisa. Su mango ergonómico proporciona comodidad y control durante el corte.', 'https://i.imghippo.com/files/hYgLh1724369826.jpg', '2024-08-15');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Taladro Manual', 'Herramientas', 15000, 'Taladro manual para perforar madera y plástico, equipado con una manivela de fácil uso. Ideal para trabajos ligeros de bricolaje donde no se requiere un taladro eléctrico.', 'https://i.imghippo.com/files/CWPD61724369792.webp', '2024-08-15');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Papel de Lija', 'Pintura y Accesorios', 1200, 'Papel de lija de grano fino, ideal para acabados y alisado de superficies. Perfecto para preparar superficies antes de pintar o para dar los toques finales en trabajos de carpintería.', 'https://i.imghippo.com/files/yml4K1724369769.jpg', '2024-08-15');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Soplete', 'Herramientas', 25000, 'Soplete de gas para soldadura y reparación de metales. Perfecto para trabajos de soldadura ligera y reparaciones en metal, permitiendo un control preciso del calor.', 'https://i.imghippo.com/files/Cur1e1724369470.webp', '2024-08-15');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Cuter', 'Herramientas', 2000, 'Cuter de precisión con cuchilla retráctil, ideal para cortar papel, cartón y otros materiales. Su diseño ergonómico permite un uso cómodo y seguro.', 'https://i.imghippo.com/files/ZHiZP1724369744.jpg', '2024-08-15');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Pala', 'Jardinería', 10000, 'Pala de acero con mango de madera, diseñada para cavar y mover tierra de manera eficiente. Ideal para trabajos de jardinería y construcción ligera.', 'https://i.imghippo.com/files/Xc2T31724369716.jpg', '2024-08-15');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Juego de Destornilladores', 'Herramientas', 12000, 'Set de destornilladores de diferentes puntas, fabricados en acero templado para una durabilidad superior. Incluye puntas planas y de estrella para diversas aplicaciones.', 'https://i.imghippo.com/files/yGb1e1724369666.webp', '2024-08-15');
insert into producto(nombre, categoria, precio, descripcion, imagen, fecha_ingreso) values ('Alicate Universal', 'Herramientas', 5000, 'Alicate universal de acero con mango antideslizante, diseñado para cortes, sujeciones y torsiones con precisión. Ideal para uso doméstico y profesional.', 'https://i.imghippo.com/files/lC4Se1724369614.jpg', '2024-08-15');

 select * from usuario
