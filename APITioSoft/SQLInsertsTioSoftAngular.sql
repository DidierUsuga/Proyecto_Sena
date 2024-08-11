use TioSoftAngular

insert into Rol(nombre) values
('Administrador'),
('Empleado'),
('Supervisor')

go

insert into Usuario(nombreCompleto,correo,idRol,clave) values 
('Sebastian Zuluaga','sebastian.zuluaga2003@gmail.com',1,'Zuluaga2003*')

go

INSERT INTO Categoria(nombre,esActivo) values
('Herramientas de mano',1),
('Materiales de construccion',1),
('Pinturas y acabados',1),
('Fontanería y tuberías',1),
('Seguridad y protección',1),
('Electricidad',1)

go

insert into Producto(nombre,idCategoria,stock,precio,esActivo) values
('Martillo',1,20,2500,1),
('Destornillador',1,30,2200,1),
('Alicate',1,30,2100,1),
('Ladrillos',2,25,1050,1),
('Cemento',2,15,1400,1),
('Baldosa',2,10,1350,1),
('Rodillo',3,10,800,1),
('Pintura',3,10,1000,1),
('Brochas',3,10,1000,1),
('Grifo ',4,15,800,1),
('Cinta',4,20,680,1),
('Tubo pvc',4,25,950,1),
('Candado',5,10,200,1),
('Enchufe',6,20,200,1),
('Cable electrico',6,15,200,1)

go

insert into Menu(nombre,icono,url) values
('DashBoard','monitoring','/pages/dashboard'),
('Usuarios','group','/pages/usuarios'),
('Compra','shopping_cart','/pages/compra'),
('Productos','inventory','/pages/productos'),
('Venta','payments','/pages/venta'),
('Historial Ventas','receipt_long','/pages/historial_venta'),
('Reportes','receipt','/pages/reportes')

go

--menus para administrador
insert into MenuRol(idMenu,idRol) values
(1,1),
(2,1),
(3,1),
(4,1),
(5,1),
(6,1),
(7,1)

go

--menus para empleado
insert into MenuRol(idMenu,idRol) values
(4,2),
(5,2)

go

--menus para supervisor
insert into MenuRol(idMenu,idRol) values
(3,3),
(4,3),
(5,3),
(6,3)

go

insert into numerodocumento(ultimo_Numero,fechaRegistro) values
(0,getdate())


