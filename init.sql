
-- init.sql
-- This script runs on database startup

-- Check if the database exists, and create it if it does not
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'PixelPainter')
BEGIN
CREATE DATABASE PixelPainter;
END
GO

USE PixelPainter;
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Artist')
BEGIN
CREATE TABLE Artist (
    ID int IDENTITY(1,1) NOT NULL,
    ArtistName varchar(20) NOT NULL,
    Token varchar(max),
    IsAdmin bit DEFAULT 0, -- this is a bool, 0 = false 1 = true
    CreationDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT PK_ArtistID PRIMARY KEY (ID,ArtistName),
);
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Art')
BEGIN
CREATE TABLE Art (
    ID int IDENTITY(1,1) NOT NULL,
    ArtName varchar(255),
	Artistid int,
    ArtistName varchar(20),
    Width int,
    ArtLength int,
    Encode varchar(max),
    CreationDate DATETIME DEFAULT GETDATE(),
    isPublic bit DEFAULT 0,
    CONSTRAINT PK_Art PRIMARY KEY (ID),
    CONSTRAINT FK_Art FOREIGN KEY (ArtistID,ArtistName) REFERENCES Artist(ID,ArtistName) ON DELETE CASCADE,

);
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Likes')
BEGIN
CREATE TABLE Likes (
    ID int IDENTITY(1,1) NOT NULL,
    ArtistID int,
    ArtistName varchar(20),
    ArtID int,
    CONSTRAINT PK_Like PRIMARY KEY (ID),
    CONSTRAINT FK_ArtistLike FOREIGN KEY (ArtistID,ArtistName) REFERENCES Artist(ID,ArtistName) ON DELETE CASCADE,
    CONSTRAINT FK_ArtLike FOREIGN KEY (ArtID) REFERENCES Art(ID)
    
);
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Comment')
BEGIN
CREATE TABLE Comment (
    ID int IDENTITY(1,1) NOT NULL,
    ArtistID int,
    ArtistName varchar(20),
    ArtID int,
    Comment varchar(2222),
    CommentTime DATETIME DEFAULT GETDATE(),
    CONSTRAINT PK_Comment PRIMARY KEY (ID),
    CONSTRAINT FK_ArtistComment FOREIGN KEY (ArtistID,ArtistName) REFERENCES Artist(ID,ArtistName) ON DELETE CASCADE,
    CONSTRAINT FK_ArtComment FOREIGN KEY (ArtID) REFERENCES Art(ID)
    
);
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ArtTags')
BEGIN
CREATE TABLE ArtTags (
    ID int IDENTITY(1,1) NOT NULL,
    TagID int,
    ArtID int,
    CONSTRAINT PK_ArtTags PRIMARY KEY (ID),
    CONSTRAINT FK_TagID FOREIGN KEY (TagID) REFERENCES Tags(ID),
    CONSTRAINT FK_ArtTags FOREIGN KEY (ArtID) REFERENCES Art(ID)
);
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Tags')
BEGIN
CREATE TABLE Tags (
    ID int IDENTITY(1,1) NOT NULL,
    Tag varchar(255),
    CONSTRAINT PK_Tags PRIMARY KEY (ID)
    
);
END
GO
-- Check if the Forecasts table exists, and create it if it does not
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'WeatherForecasts')
BEGIN
    CREATE TABLE WeatherForecasts (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Date DATETIME NOT NULL,
        TemperatureC INT NOT NULL,
        Summary NVARCHAR(50) NOT NULL
    );
END
GO

--Check if the Forecasts table is empty, else prefill with some data
IF NOT EXISTS (SELECT * FROM WeatherForecasts)
BEGIN
	INSERT INTO WeatherForecasts (Date, TemperatureC, Summary) VALUES
	('2021-01-01', 20, 'Sunny'),
	('2021-01-02', 22, 'Cloudy'),
	('2021-01-03', 18, 'Rainy'),
    ('2021-01-04', 25, 'Sunny'),
	('2021-01-05', 23, 'Cloudy'),
	('2021-01-06', 19, 'Rainy'),
	('2021-01-07', 24, 'Sunny'),
	('2021-01-08', 21, 'Cloudy'),
	('2021-01-09', 17, 'Rainy'),
	('2021-01-10', 26, 'Sunny');
END
GO
--Check if the Forecasts table is empty, else prefill with some data
IF NOT EXISTS (SELECT * FROM Artist)
BEGIN
	INSERT INTO Artist (ArtistName, Token) VALUES
    ('John', 'This Is a Token'),
    ('Sampo', 'This Is also A token'),
	('Saber', 'Token 2 Electric boogaloo'),
	('Archer', 'Token 3 Electric boogaloo'),
	('Berserker', 'Token 4 Electric boogaloo'),
	('Assassin', 'Token 5 Electric boogaloo'),
	('Rider', 'Token 6 Electric boogaloo'),
	('Lancer', 'He died'),
	('Caster', 'Token 22 Electric boogaloo'),
	('Ruler', 'Token 12 Electric boogaloo')
END
GO
--Check if the Forecasts table is empty, else prefill with some data
IF NOT EXISTS (SELECT * FROM Art)
BEGIN
	INSERT INTO Art (ArtName,Artistid,ArtistName,ArtLength,Width,Encode) VALUES
    ('Fate Stay Night',1,'John', 32,32,'db851fe8618bd4b85016a96c418f1eb5e7a393e47c8d574823b654ae763d46229dffe4297ea13b966adb2e3b2d13d89b383577098f96718f107631e79c67a406a709fa1f8b812cc83ec45eb420730822ca054ea566a56c9bcc873346d3f3c0e93eea3df2124b2d4cf75c111053e4051da926d074dcc6ad19a3f4ae1ba348b5f5cf1899e997c5eddd5e3dc832261100b1e6b5374b7553688e49ef9050e4ffd5c4169a93279c1fc116ae08b399b2e74f758a1d6793e003cebc99dfd298e063fc21ca4acc6a5eb23b58904e05974f3080035cd8b04647e0311fe3462218205cd54e81e4f10bb246b80fa25a86e40b6fd9779fe1fa9ff21c4c3ebe17cdc1ea49426bdb8aaa4077f8e7fbb52e5f33c68ebdb3a7f293422bb3f4cbe7d3e5b19f0bcc666a625685f54b976864fa783ae4a5986d959670706fbab79ab688d6f8da5a8726b7b84ba09894f2030636a5b1af12143d23a431baa43862596de612ef666d45b9a7e727b627dc8c9b6976da84cb151a73e1b5151f0b3a798d173bad773172aabc94a741e8fad6c9f81a88ca6852c5395fd9dc5b9a3c0ab5fe5f59db2486a0f3044cd84667ccd0b9e10541145ca8b4c615164c8ba83c151cfe257e9fdd5e541e4fbd942a9830cda0688fcb48cbc3d0c2230ed479687d91ec5c5bbf6acf6884016ee91d3724c31ae34ec95d9ad21f37b7dc82ea5b105ad4050307e8e3d9ec8da10024a01f1985c08ce14ab475a75ffc5e99c50d8d6d9378f3ef53bb57878a00b602cb44fc31518f44768ec34d8600a2af201a0cd3782c06c6d8609bdd04cd95761a594ddc0a3db3a2beedcdbfcb7db32379d01dda2640beee297ade31e1bdef6e33afc710d7c8cb2d7b88fa63737187dc14dae53ff6d2eccf6f24eb4826b274eadd8d7accea12767d7eaf2601d2dda3a434d7cf5216d2d1766fd9e02f383a4d726d64128bb95374dad3b9875cd192fa95d275ca4c28c9a8edcbf72633eaebb6ed0e7e50b206796379960522299847a8781b01a64a4cf40d8a44a34a91da229d8d13cb24a14e201acfa10260e9ac61cc33d07ba23dd51a789e5380c1441185959710b1a8544ca01d8d95f0a07a56a86d58ce636aa01d88a4f58860f1f6eeb8596728b37a2671ad8cac09a801ff7f99fe8455c2c60c187cd20a089b8a52d8e17acdbecef8d9d3365b99289c30cf56eeb665892e2af61058664f3eee3a0c24ea0a96aeebcdf8f9790df1452ac0774cdbef9099958ec40e8c456dab30ae815b3988762149875468b20cdfa8ba1caea674c23672a94e84b4f05b86fdc6a61d98ca7167d8fd6e84abd03086ce78e5a9e3301e67188f16550308a9ee4cb47e9fbb69991037bbfcde8cec352f38caada5c3dd4d336a2743430cfd5f223e425a4e048058eb9c6238dec7aa49de9bf2bb4dc4544e97b26cfd125f4ea680cfa02ae5590b1edfa82c1a795051a57760f9272bae951b5db761984e36dbf4bd4f2e3f52f74bddcee027a50392210eef5dcf61ceb3008fd1049368bfffae14448f6b2a998e581f2393d376f691b222f31fd3c1649427426aba5365981aabeb0567ba63ed586dcdddffd40c72e929c19a0f620f173bc4caa96ddd58d9ffb314459d9e0e358942e9e69713689a8fc80ff7b201e068883c9a3c74e3021e88167c2943a86a501ea3cfa3ad65f3a18b1e8dc8de391dc1e895155775a96766aa62c7f66082f586204d5d52ff0b5b54b47ff7353fa48e797daced1a3e7b4e91123007f2b44f122ba1e94a7b0374ef838f2e7460b9d2d373b7852553de59e48de0999d08b19adace506e7eb83fca0cbbb70b8023711eaa74a7a7224898842ec7d61feece3a58e5c3b594bbd85c7685bb9c0c8468c4ddd9ba4cc58d418961b9634a043e65aae1610089b90547d33b267b57dd7049ea9c25fe2c327d62d11a0241d85fa209f7923af2449baa9206475846c033e9ef1f2da02d35498ed3c8c01e68de103069e8e8dc9d1bdf7e7f3a738c3c82fb07589135d8d181ec564139ede6682516889f3d7e7f0b84802fb116f054f85189844b52f70093124dbe932eff24a0526ce556a3a46b43963cc5983db4246a5e4f45755d8094b9340e9f585620e233162db1d338949ad01a56a255cd81299e9d0dd9127ac08e6af7df623a27ab32021149dc02e6608cdda786a5d8d8814c66ccae91fdc0c86cb200c1f70538ff1418b84fb94bd4a62e85176e3582c1a58f1fa901d7267e6fa138c184cadeacda8ef73d2714b89868938a4763b6b23efe8b9c58e0fb3481d27b60d2b5c10b6bac00e00dc87ebc84f28422b618ea443f14c67dbce4d9722f71ed0b291012c3a54b4af71cf948ba39fee44d012fbd8d6efafaa3d7c0a7b839669d029d2b40155c4b796b2b7e06dcf384d47f55b3065e8f0dbf1cf7e45ef7eb8c2d431ac76fe86556f086b3529c50578d97f1d35216120d496686f4caf2b2e61774aba9696864a6bdaaa0d038d01ec0dc04d0205cfe587dfeb22500fb3f8d3c1d629a66bad2edf2050cd68af4b62af00d50ac87bcc1443c80dffa62d793e8d0e9b4be973add1891640cb154d43434078c93e533332a4279f940210dc9410bfe2fd925bacd7beb5b83c02c5f214844a658641e4847a4afe8caaec9e3a62f862ee2e655580bb31470c4fabf563d7d436ab91ba87638ac6e69da66ac63b359e4da76e5327154bc2ba4e6a2ad67cf54d10961f2935f8cbf21e913e30cc8211a80d512fc1f143ac2ea5610ca403b7e68333086def4adfd6ffff48aad8edf03d360d2c3c84e64fcdb32576cf9715c3501713cb092fe2862f33425c5094823ff5506189a5c163603316332b9173e5ea832e5cb9398b8a9164b82089e77121d77c597961b1fb4db846ecf4107b725c6533816bcff9c6cd653f7e1a5f456b64ab6dd75f2c859eb1a845fdc34ca4aa87e14453e18685d87bd650b7f24f89c1921060d6f38423c68c50c86cb251334a3146714ec66c7fe32517a0309d43fed317f1ecf0eca78600f75fa7ab8670d2fe20c0cafc935e987603a8d725be1a0265218dbf1b644de652ebd3b777613059a303b846a4babc6e045460d756d9a4b661258284d6c2735b3a2bcda6cd8f65f6119cd950a7fae2e78f97f80de85e2ba7a4e7d2f854b41d23882d5f5813c8ef597cf007a88d6f379ec2abe860dfd7b359f21eb91be63c8fe72f482feabfc86dbc9e82af54262cf1a94d2e7ffab63cb65e89cd216d087b211efc2c8324d53f52a589825888cc456e7785db0485023ba53ece3920aa9d2e6038a267d6c70f776b64683e42a312e1df0c3c9680b108ad29b86084c01e08c0df971e343bc71c012e08ae46b90996954af8eb5001fc25f88b9bbb24c5f613ce4494c0a8fa8926b012c1c68c13e8992bd913ee5442f4774eb7ee0bdca9231771c498bb7e123ec15882ed75f0bb7096c2d8d94ab3a3437255ced1857e787065aa14c35c33e3341e6daf8082b4e965b1f5a8390fb2845af37e932b9fb6e7d18a017da5a71a6193ef5c9e81c6aee6dc2729eb9ad7137c8a7b68b9a05f8d282eae7855cafa432d0df2c866d9f9bb31c8b3afc422ef691791a1f7a2b2c2823cd8831719e4600889749c414cf5a483550458d09548fc0379b014aff0f04d57c306fdb781d9bc75898cb651b89efa472ab611913808ed9bd1b9ddad4d49c18039c7cfabd69874eae256850d7305e5996364b90e7b04abe8be05e22e87e6c17d52d8be27996455a798ff14a18f1a7d73b6936cdd4f3eab4a98b945ab61efa2d75492426d7cdf022a63b5ddacd303f2bbcc6f3d9e3c82df70006173bc39a77aff3b390d7ef0691e2125632a11e0fe940a71d5a74076063a2bf70d067610f999ffbd81683905f81c638fa22e92261a482133c9ca218784ed59ff68b130eece57d0ffc20c93e4a7e33ef2fceb470939908c247b1ded3c2e3069255634d87199c067697b70d8ec2194e4488b0d417e950424c57914aee953775568f160ce2f177cca90a4bf026c33209ea34c78efb4c845a528ee7e6c6375be99d27123f2b892083bf3ca99193d87cbfd5e1d229410679a9b0c2d3d46f6c3b3ba864dfdc5c8ebd266a2fcde942058acb339db5bfdf656ae54cc26aaa8c2383573254ea53df306a2a73934b100267592ab16f0fd198c493201e02816c224d2e27d21f9ed495e37ef0b1303f58ae6df94e3fbaeaef89e82dcff1d461d10fab192706a1c1a0ebc2c2b54fbe04a7723b7259ba57485eb674d21522152ad4807798a65bf62726217d5564bd25d0a2127c16526bea44fa51392a549123b79007bf2e75b0f88c2dae79122ab5d8ce25818d9f1836984caa06a9b9f9417a22ae1a4527d10b2c990324'),
    ('Clannad',2,'Sampo', 32,32,'dca100e2240bc4d6513ef2d556068d7c0a0f54aed84ce0cc8c4b406c8ae8f3ad1911f4adb6e843b91923d690ce36132c24e23da3f9521715b12f46156d88468308d13d0a75639809613c6631f386affa79140fca25f619bbb37250a94d0ab0b7a20d81050069e250dfb678e5b86db344c9f0303c3d71998b74a98b2fe26ca11ed7066aeffddb272c97df3ce96fa303d09ffb098b2ff86f8fc81855c34d3e40cd231ad91d231b3236ef7925ce7dcd893116945c3b48ca923f8af173076f3eca328c38559811d98ee58c49eb1e203c1d6534bb596f9f14fcd8a4c395032a9b7e4a9bb76ebaa6f236f74ede71aa8d86526de30cc152bbcca5f5856cb370c548c3d1aed68054489b8ad836e06446ee6ddde664918f1395bb276bf7d23d72f7e8bd8adbb795ff068661f9689914772dac25d257eda618c2f4e48c7f1f654fbe374dcfb210a7c66d76bfd3701440413dfb4f152851f0bfb31ced1298a4388302b2d6e95f130b1cdfd852c31d89f5a978148aa03b63f924d0dfbf7299e15646cf04b282e81d91813b6762d9cf19d28559daf13d83b8b6ef5cb1b0ef805a50dff37605199a2ef9690ff1d84e9a04b4d16bd67d81ed6a1acbab6997f8488ff8615b7196d5df2dddf0a1edde128463fcb3adfd6c05522c74f7b40e3d79b58992946e94a01eaf539e06e150415a6d7e15af724b2ad37788d0405cdafdd8b929d68f84dbf9194c9110b7604643d011870e72ffbaa5ec631b570984bf528a2c504c312e8f20e31f72e277ddee9618e7e2eeeb68d650cf340bc059c64530fd245344c6acf5419f982567c2b6f0448919e9797bc341622cc4a902e75f8e8af825f0e7e607e1e16f73289256b21d24db0e0979babdaf50d9c7c995ed39bc81540ed6c0223b65a276ef1c543d37019f46695e86e57b149ff315c80da851fbd52a3cbd8a966d9d4dc3f0c630d9aec07c152437b450516fe7f32472e86f03a86224c3e3a2697e5d1235396e9633406a0e1858f033bf6e0dd456d5f02746f2eaacc6af761ee65dc816c933b0db68812476c0f7e3eb2c0071c394dd971454b6a821d5b09d7c16c18538e174942015e3061473c6a1d9b86f29afa4dc123f005f89a557002487895929ff4ed94273a4fc05c9a9edb5c8e390e49b4c983772cf17c88fcab3fd07c022e1ae4693c2ddddae53025324e65502322422fae3478970cd4534726fe74888cf72139621d69c7f1817205b4eee2b8c10a4e3371bd94dbb6247e8a83d3e9aa7067bd5bf3908526ed426cf976c0bd1beca74916ba5d353e397670908f41cc8c93f4dc04cea2aac0d41681a9cca1755ac924f80e7879132ba836734a2c38ee9c7bc89956cb85e3e77b53229a6552610fef042c1775ad0a155a954b190cdd51f9bb783fd5efe78fe175b17af9df290093b1781c7f17da5d19f9892d7439996f54519c19c751403d3e2dad5460ee6bb9eb7390e7cea9d04a02fd8ed8c4c4194fe5f2ff042cf9828ea7b8b9b33499b0104e4ead2e4c0344f493e2441ce95f691c9909ed70d65fe4f440d7025753d754fff910a0d7daf340295e68b000d302127175dbd5c985b7fcd877900807b7855451ee28cb34fb6bffe847746c047cc6a48c2c2e7355ff4d89d9e8bf1c2d7158ea6e46350a0273c48e924f0e4cf25c28e81824e282560f53201425e3b7dccf93a04cb9b7714a96ccec8bdb2c47eddeb021a745af79e03ae7d55ec07da1e4ef6708a0839b67e9692f9ddea7ad322dcf86bca9fd2ba297a58da27f9e7a1c91a7b86b70e5786e4d1d1c33c691b1a7d56db97e8dc577da3b71be40390c3cbe6f0ac1efc4c1e7ee859b38b90bd20c9eed96594f9ae0f091f7e0a3687f6f6de5388dba0460f4f06adab2335903c9ee0fb1e245046aa02f6b1ad457e569f0d686e91bb0dc4ccfd2a879077fda85e9f7f3feb4d4dde948dc7377d4f38f4f9c754cc2d669879a26dd629905fb6fd8b1a99f09733a77d4fe7a5e2cf57015221900bb369a44b3c7b11ddb7a0a01be750f312324e174027564dbe7cf87f9930b077bb400b87b14367f0030226ca397805b553b3ec121fe5775631459c00b395434e00a94c517d9fa9ba7377ed0c21bd13335338edd5d9b2fed07a6e7dfe2d31bfa7052730b8408fff2480063f4cfaab301a2409c3e55c7f52ad0c257740b428f2b42521c7cb725ac4b1c50931217403843e4c72ec59ff0a67fbcc2397d6f6bd72e89b411ca502600294e4cc17e6754ad9ad9d2f4b6aca1a7a15e5dbb204b9e94165a80f48b6932189a3e9a97130c6de0997d307562ae0b4aadf7785274d4bd630a33566946772fc492434f46cdb0fd90bd3d7f0a91d8d9fb8cb821c90a3637dc781ca69ffa927256ab7e3e4642b7be7b504b44b3a27f2490748036ace87a25761476762aa28d78b5632adfcec3fa71f11fdf400e35bc7b10ff1475720e1e2d9384cd6e7413827cb6ffc0cb2a73cbbf568ad7c86a563a3fcbd6f96200a108d048fe277ef1c2ba90a7d8979261e0406e516ef0272bc4a4f8e7c1fcc6d4b84e9cb2e74bab0a151f91c092a516050dfb60b05504b00ab97a221dcdf01fbc2721f808c97918ab847b30c163e213d7581097c88d55f01085c2b89e588fc4ed18feae73c8c2830c6306df9a4765ae81f765e8b2f80663d451ff8bd371cd100239a6fed1401040a871c9d94121d4a58d64785cc38d7db0b8cb7e39a661dad43378eaf63d2d3be27f219dea6b478dd3f1c45060ec2ba7fe7349befd7b01acb7618507dc213f6b3a6f60db6627bafc5ce24e85f8053c6ac6f451531d4388b19929a06b8db7a98547f6b9dcf2044f93e4731e8532a3c858ad4f85f0fc76e0bebbf11582ef611e7b6a29da8080663b0bc9a55afae8b90c6e2777944458fece8c26ef7cfe3ab1f3873a97da977a88f3cbeca2dc71b50d621db5b26f490d0cf280e49296d63c621c4e276b763228bcee1951ef3bb21c30e94776064c1424c2c62c745f367ad9e87850bfff191049220a5838d298bf87cfb866472a653489aa568abbea7937649142812b6031ad0a2ef252cd4c4dd78564462bfe131668e4399d59016abdc0dd97064d0a55cdb673f8c0e35bdf6950a25d81ba79dae10d31134c9ae877f7ef7970f78d59d42cb634913cae027b4d1954a1514843d5e81acfb6a8d3297f6714cd30e567e878e958907a42d896922906712b0544c8e7667046a5e3cf28e723e88297019713788ae56b0a5e8757008c16ff015e3d030df690f809c709d592a10854128bbab8e899226b239bc12c8c61835f5c62d805616761ec1ca579c5f6f64e7dddc125ea3bbd286ad67160fd65187f90d9f16f0b64d0d86d83a3481c089e4a164abbc42dc7e306a813999df29a8b97e4c2fd28eb1e880d49e8e14404cec148c9870e48981be2fcc6b2baf55682c78503a99fe86d509e5c46f8466134ca72161bf162ab90c2e87432af183688234bbd253ae249764b7456f8c35bfb06e0069c92469fd662196884323f7db8700b4cda598b0dff825c29dbc3b4cee98105e594cc2d2d388dba6e63f9e4c3fd42649c80a2c8ff104a186c5606ad061e488b6fa72efba297aab3fcaffeddc4c87254f0e43348b08a27a15e57e7fe6b26ee9a3dbdd5adb021a24ad7c819366c3912428b9d4247e4502c1e062200aa8ffae1af76b8a6ea1ef526ec039e8b841582fd924c2c6a1d69875ef07f3f416500f5a76d22f7ffa8f957814b84764e359cfce60f35c00413c3c1d0ea1bf5a330bc783354916d02f1deda8b42afa4166cfc37c9f5bae3d218d835f68831944aa1027e8e2ceb86b5cab8e72073312ffeb781bd5ded3f63a16649c1aec95da9e3e3928611633758a4d415c09a658f7215c02856720069917d27e40e7b0c701ccbe89dfa9b634a9367aadd2ec6452d093f774ab477814aeaa1e0ec18a5a525615b138d643d78822f613317a170866b0d7d6f7a2a6d0d3e38bbef1b490341386d004418accfec4f3dc1113085d0d425351d7cbc46a793ac3f959270eeffda40d8a679659dc035b8de56f12ac4ca6080719c88f932ecf9c024bf32c3d69ad6df6fd555fba6d72030b5a1e0e63c9a6d1558ce6f05fe44e747e4dd06d9385f211fcc818ef76f03ec061e8806f5f80e57d1c7ea8f8a0d1cb4445e5c51647aa52fc7685b6556f4d80afa8b349618e326a681c79033a39a3dad7e064a4460d3119e08703ff806c8d348fbaf2a1c5f37847f2cb34e68d2c345ed09b19e5b282b727db234b2765f53c3424add71bc95b4cd0b4be40df08af612ed6f5013e86b13106797c0d13bd3dc9457417ba079819250fa7e2c7141704b07d703d84254278e122bebafd907bd977f826e3'),
	('Title1',3,'Saber',32,32,'26556c48e1c4690fb98ee154247f493fe3a86e7ef6af6357b077bc7962a0511aba18ddeab11fc3702f012aabb92fc84eb73adc606b0196a327cb39f9f83574bea1bab91fb3bfae8a125ecaabd415c45500e100001556ae5b0d09e3e640375052b41130111d5fc33eba4ee7924a9107570fa68e65fe26b2af59aff58894d143cf7e81df3e4d74e6c1e4a1be039198c599bc97966b48485e00f0e742a2902c13c3d6cc82f8cf3275daaf8971c8278bc44f74c76cf83c76b025290377564ccb947a6ead8787e7bf14bf69d1897d144ba569a07282ee397163ea2c45970c6f4c0f29c2f999f6ae46f25c9c13882c2ca17e78a4ad706e8632db6657f35c6f8da62e78240fa434819205b8c020d50fa7bac58d4f5d36603ad7dbc354de9cd3ac3f7e326389479148dd03d8e12650e1a712486e31d79492303c3fed8efa919e80b7bdbbf2b19246f008a7b33997fa0f8a3aca088dadb6451d0dd9e40da694957d9b0de0855bab54456360b18e8a1dc73c17e288207128d69c97d5e032892f113c13b09d795601e3e49d644e47a0d22dc641d980a9957944ba87a0d6dfb16dc4ae20d4b8feda48a657b216a9e6fd8ee6f6dfc1af11cb7cf7d23d1bd641b32a58c849a143a8b23e9a0d06108e943aee328fadba034169b1110d857fec4d548498e07d4209a1d3227b92e20158d1626e293ffb5628c712322e807f953611c0c0ac65369d6e574a45a260bfcdf1d0b43ba2219803de8412290a74e4783bdba7e40f2ab2c173911b59f999e7806e0d73d7ee870f6f6a9e0fcad746ebc68778df870bb8ce20f8a927f0d358b5298d70ad4302ddc6b1a114bfbdb7c4005c9a3761d814d07a75f1e4ab563ec383238fa4f76531519308b6a3a96e4f02327a806a6389febf6f1ca6f67808f180bf7875018c2919eed51f7b7df1ab5e37e0291821c56f5d568e45590d7fbf85ff4bc0ebd776dc7fd4f675faf8899dd1b072340ff22cbd324001319481602338458b27d8782d4f549f5c20e713f37fd29d0069a55ec0792c003b8dcb919371d999410405b3200a7cd99e6f9d18d2e1d6a0f84900c4c856c28df5810ad4cb735bb461a29439392d855d1a4df9582c66f5b56806edbf217cfb40ffe8f62a5de7d05d684bd04a0cab81078adb4ffc2cc84d9b0aff1c11d41417198eb4b0af362d026edf799347242be1b3e8636ea53242353816aeaa78c08f1193992ce25cdaa8ddc20b76c113adcb290d32b50b79ef29f812fec9548a2ee46ae3b1370f9f07e16acbe80be72ed315ee18a7d0a4e0dd494303b55ac91b948f8c6ec9a06049405b210430aec3a48d4efd89a143fe8fb5243dc74b075a373b6014156ac5821a3ffd960c429e14d34a7c76b3e06e8b6480592cf28a18a48637668b7fc4a84e4c67d84427a917c9beae7239409c43b3904de6a5222ee352c49e9983b68f62a2edc8b47ed856c64831c621263966c1992869d5320700a1c3c3da326aa7b1c78922ad4943cd97464df4e13516c3e9fe2600e7800d281f2713e036905c8fc4929711eb0e7d05d97031144fdc5530e5af7105ae9ee382ad0adc315a78311144b83b722ed485097da78c165de3bebc6001b3f10966b321a99e553cbaacbfef85638eeefb8659d1a589067358d3be2ca47bf7eb98dfa399ffb85099f9e685b3f56d13029c11569be7664cb0785e2d844349c8208732b1da2cbe7251d87bd02c1b96f2b53fc5bd75293b52b1649873e60134d8b97dc3dfbb6579237fd12991bbdfbbd86981f06d96b752cb58f8f3aa8ea753132ef4a84a2133453c2546f3cd3b54971c7841d1696eef3bdce0b9efe5d7738b066cec493f5adcafc5bf9caef32fcc4c0f030fad354aba8d1d3762d8ea3ab3e1971fa16e7480c0e222d95fd5182e05f965e416400f7e42303523dba0207e91584a5aa916e972ff2f70c0fbb1193558730fd2fcd5d26ee32fde6dad059f1540c5e0ba3be99b0be6d2e3ef46e2891d4b926c4c2ab1efdeb39864ecb2848f8135490752707e2e08677a6bee75d4059001575bd36be989b79ac3015a79cc2f383d687f7af97cb981fc610516b31f5885c72870a64b160a022690da5b3851e79242e633d6792088f3affe135c9e21bb79b8830a2f2f02d10e19542a4b7bbeee3c32a8fecf808791e45fda1bf8cc576a660672c6300414f188876075e4f370f334eb41d13723eb1535565c7cf7dd13362404e388dd1933d96c77266189d53a723a4c287351005bd459c82a25b93113c71cb837f0e5bcdca5f1fed700cd94ebcf0545ff502e5322844885df55d74a23637660ddb331a4aef768469912c6ddf72c1a09dc2b166f7226b6f836b4120eb1b0a88a3a7e084ecf67774d455eb3728546daaeb4785a76e67b5c4a2d271512f51c34a327aa20fa512d7bc550ab63eb45f073ad4204b7ded39dda2095bf3a70a6fb8686d0ec2dac6f7a665d8ccc9d00cdd7949807285f4bec67650989b98b5a64d06b2b302a8c23050f16066dc37a4f84300875b39f3564e519202a9d7aa0a80c5baeb16a75d6c64334b8d637545d5b157244199c8eca1901458a9885310df7bda281e7c63be443c0b7a3cc94b84c2086510d3c837f9a38180a1a576ada89e0abf2a62dc9d0d2272a8cc1273343edc92b39c3eb4ce408416bf8931f76ad58c0a0adc79f2a36bb40f22c3b9b153410c71ea5072b219ff53093f7d89cb6256501178e9faf8bb854eb56f51033195fb1da196ba904ae0595640b0ea1aa1fcecfcc1e1787def7edcbefe32968c83889b00f7602506d42009334c6f0368d3a3fe89765706bc5e3df6bcf36de68fd25975f6fc84e787dcff4884dd3c956cc39f2cad413a8e53db3add53be9774d58ba986def0787b2af0b5f1bcd41996ff8cb7ef0c3ad07b84a36e82708bd9829b17095531e030b2184f2271cdd6308529f6db180b867fd5591ecaaf8a382ab5e7961f06065b268e6c4dee1a2320bcc50e9b754fca6f8a14760284749d5938099a84970562e95b9f8900c21af5fd8f353a97829b099664eb86ebc417d61063ec4eeefcff0fe2fe4fec1c15d60136c0caab13d8c121a5b8af2bdf4dbaa556b6882fb6851f5c7b10606687e67049d4751e3312fef931e3f184c2f20d5938fa29c03a1bff7e266473e334c177a43bbf96d248f250b14d7521f2aa4bf771ccda44a5f044f868df3a829caf4a098b223d74f6c4b276f393486f01f590d9acce6bfcb4f4c94963afae3bdd022127b19a89de1e88dcfc9d7f66ba9d096bfe5dc0eb8d454426515fc0e07191abac4328fd4b0e206253f4b25235349e4f432dcd6e28000e159ff7942c0b5c8098b3fdaa72026b68f56865f785c6d301528bbecdad866302859097753bc9e5fefeed70e84e4c8574e11261183cd994968bbe8a7dbe5c5a28d90d41af8d2d6f88479b685fac316d66e7cd853d9afd469ff89db0245f0e53f050208a05bf438c2d1dfa5fd3ce907be117fb81471a3d93a902a207a1d88760522fad9d2a6be6b6faa8f4b8338a68ee4eb38a1e30e187f12d080fca162a0aeae9c43f0afa917af3651fd036e28993c948574a467d40288e5d5395abd1c4e6359f46a603b64207d9cdaf7e0fa011c654024c9712b4306cafc22f482744ea03157821816cc0ea7b0798bccff062816f21b15d9e5ad550e8d58b5ce5c4b15d736e40dd13b6d8f36e5c7c22bf2f76dfd0a4ba4670c685a38347146f2640b265ea510553f6002a37821599a64d326b3a429f2137dc6bba4af2cb45390623450d53001e6a42714f357e7283f9ceba751eb759dc0061c8581e2c4580ad986ca5d65561078b108141cc26d9a9a647adf48eca127d3fa03cadc713a48e282aff3ad879cd41e538a34795fc8bb08d6b2fd00bad0d05479e252c506ef1205b3a3f77436841d7444e4ae0bbe99f007205a52b0ac55d5efdd578ecb23ebc266073656622faae7e064061fb7abf779d6f9c4aa334bde8f485e75c126a7ef7101eb7932675abf628f099e7fcf82caf3975e5b39fe6cef6bc82d127e1e4a356051c00ddbb3368e63b39f9692922c1ede7d18aa724f28d648659ecf84b567180b8123564984c169b9b42a0b66ea0da37f2f51342de12d938fee9b866774a41ea0f7656f469d0663ad0dca125857fc549945b9e424d6f5b3640eff06c8cb3518245bcf02bd739abfb16534f7407bc867e4e436b06c6b0d19205b4081e6b948583735346e5af952f6911ba4462576bb5e720b51ab82c0617f136bcc8f3f107f173f155fc75779b5ffb359c6457402e0059c1ca4ac65c5e5ba3d9e968d19f8a2d8137703f2bb90800cee836986ab90474f32de33b3fdd991aa8bff3c064723229a81bdffa623'),
	('Title3',4,'Archer',32,32,'7c5ea51c97099c2777b9c93f14108cf8dce106aff191ab52af5b3d99ccc274ae0c6b7dbaa5dc385f2a6099dfa2658f788349f8214b37fd983d82885c8e042742a9b6c804bdee38232ada13ddb04d50a3c2696535f760ce63626d08eb07c03d4879c9f60f52a3b702b679ce3d7ef558b1df54d6d229d224405a8468d98607aab2ed6f56b70bc3ab85de17f8dccba89b5957a50d7459ccfd3ac22af86f954ef9f189c1246bd65cec2861f53ba53d68b7911438467c952b9d8da581dd05a2923fd20f0ce9078c43bd398034d9b2562c21f1d7ee1af0ed968fa4ea2d3d090bb67fa5006b401fe681e0c9e8db5c639a3f897b13c7aa10245a1ef0464e8df2b0ef99a4779c8a56f43134fca9c5db45080f72a7b4da6cf7261f62e20217953a582ae48a7fc1d39fb70a466a6d6d79b23a63eb9d75a4da629e7b4d694d14fdf004de4ba0d519c89fe094b63c9c33d210e5b394acc66d7ea0d7434904ecd46fcbc0bb0915553768b73de7d0a8ac45ffdad5a8876917c48ddc3be81cb3af1768deabf58bb7ca505b900a8d4834bbc0c6db5b7829dee12547b9cf9379eb9a224a8d1620b547eee121f0d2bd58b059c4ce5b58fc54122ffb4c9e92c7e961674a23c6f843af027a7bd1904f7697c67f4dbfa111f6875590561402df1641a97e4fe85e5eb10bb33d670ad8e3cb53cb9130452d1b0b4c3bb11f9435ac2804c0232dfdd6a23810f4abd4f5a56f2eae58d977a3599019425976c1f781d9274b05e9d4cf5b26cd811e7dc94b0c46852cf74082945d879c9c4f84ac57aa7e346c2c9a711a52f0fe9f1c10bfccfec4481a53db1e8f87d426fc647fc8a1af11b7e5dc0436d61ba2b5c7c3e02b8f15f2251d5175f865bc2f1ad1b6cb0cea5c39241c3c42767399b872d4e492edc01398aeadda12466a9089a35e675712cb0d7d6847c38a524b8c458153d1dbd963c8827002ebc85bb09653590f09c4db1117503891ba6367444c9bb5e41ad82cd1881c16f7552778658661cd092f29242c2256d5db0a3853ca9be93ed97e35452a78ed71fac7a85cd99df8e7567194c86ce5febd5d7ebff0587c73b7aa04e607109e6f713e15ac1b298d5364aa5e6b2556d746b53f50e1efaffcb0141bbfcfd463dd4ea7ecf305a92390f3e5bff9a226aa3d7bbc9b3185f6857595933b2261c612c979bd244a6d9e7d1545532490344adc2604cf40e2845e927438b13f2884043e28831eac89b5f281c948717d7cc5e18cb4a476ba788bad52b5c85788d53ac2726d2911bcee0b2b0d6a0f87082cfd90b15d26c93698b6182a846c76c2a40b1a470512bf6b97a1eaa4ced34c92b975a4f93f509bb437710d2f9e5e4a68e656b0675f332719a9026e96f574f49905960604753466906ad64278deef33aaf832794d8f5398c801b7e2adc603df17b68d622b9a2a16253e0ac9ed0059ae57b27e4ff81a25fb4ed650984e30d5b17a1113b86b5b4a44f474c5b45f3d002c523348a55c1530f0f64a308c59229a2a83c36144f7033ef043c2e4dbe281d566ad1e5827dffb41aa5f4c789ae15f6a5ca2e1a256515b1924620370d9e0ce2e260a080f51fc42949291ca237db4b16891f87d791069daa1c7756936193257573e0bc6181c92bf4adc0fa92fe04bfdbf322fba704109d1341a5f2dbc3e501981e134a8eeeff976e57533d87c6e2588fa2f3abbd18fcce27d35ad85e062d543d084e798d2e7ed2d16876aff76689fb83b23c8fd91c464fc63afb2d6fd904cbaf508953f88dce5fcf1b150616b096ab725923eb0d29ea5a7a172e638930573f1a4bb44b7da74a09902fab891d9b9bd86f793243e66cbdac2a7057f6303ab4a65edfc59c96972feb1a8571594f4772fc08262c394471ed090cafecbde87e1b2dcf41fcbec87c27d644a814d5f818431bc67f76c01952fad9344ac392fa25d70ba877664424abd3e50289055a2d9d92d4a27eaf1fd9d6574c5bd300e7f22680b524e7734c3620f5f62b9ae564dd11b67714a074b4bdd80847878d60a0a9dbd0651683b3ab09fd91a0091bb63846d72cce17125c4ed1c9a597c33af9c754cdb6fe6066bf0e783d15db8698f667e752512d1fd55e076df9b747d363fe7309de35d545f37c24753dbdd686eac9c074ebf18ec482f4d77a10e4eaf937e4b0d6f795d66477574d9d96d7db0dd65c49e96b27eeab80fa269c1fcdf60f3bd064f23481aac51237595e3267e77006f087e0d30077fc6a98ad09c2f3f048e74a0835b5673050da167b17827059aef49da236e213ff662d4cdfbb1cf4dff8bc296ab77a1f5ab4ce49362cbac1e315beaa73232b2f847225896d1e4a5b240097da28f7b5434a976e8c795eb10e494a07864e84927fe48e49ef82eb9ad5acc654d3b43e203a3b9361c9149fa123bbe8acd6b308b3ca8277874a682e5a6ddab7538facfc4bbbad69739476acd746b81d928c9594886b7255be4f3c004bbca7519308178fa851bb986e8638c6f205b999d78a7892d877146e44d1407a0904ceba1bb32ad765541f683917d8e30b69d1c20e680e974073b262aafc7f20ef25c2fdf530e622f12370f0a12c76414ae103ca3b82dc1adc3ac8672546ea04e494d4480ef93f72fbdc40b27cd0e38eeaf5cca9b07937979b6bc524439c9db32521b349f9e73b3f6bdecd9971636982b1650358c2e46f9beaea83087edc5ef5df0ae5dca8edb627520b5bcb73891dbe273e72c3b5f6b75c082cc11862c54bbd06e0b872c277951a86cce59e44fe3da5de1eedb13b902b8db77cc8114e4644029b68bb0719009eb59321b08be8c8872a711f3b5ef430f0b180d4b219b12e636681f082f75dec00bac38dc322068edade060c70789d39bc0ee0514e3fadc1fb4ad01c365aa97f6a1303b041234b920d8b72069d07307d2ae90f92c354d9330d598c66b8a5454f551301003ca2dcb0fd089453500f3739de2299f5404094e2a53fbde861b91e77cba8fdff68f9c3b01f83237ee13ac429cec5c3ff08323c342fa316cd1ea816d9888fd2d57000a17a73cfce3bb2bfd369e18c0b5c5fdc9c12647ac51cacefda2785beb6abd05db7f1cf1b26fa3b16860e2929d16ca85ac0c8e43fc0c0854d4253396683683a00f2255d25c1d3daa7c4e690cbca1972239073d235f9b44ed7a14dda4169ab44f26a0fb1028bf9047d83f0ef7d0305de512a9bdd75c13e6028165fa0c4aa97a69d4fd0449ad52d2a1090a9e75f597850ede39f3df2135f6c7b4d0badd4f45650d4dc1e866c474da02a723037a2ffba8c0ba0ef7614701ef6897523e5b939843867deca6c7946bd118e0dd5521f0318024222547de3978600292a63fc569846cbc5847a10eb0ef52eac7803e453a80b3aae027a604f73d9924306b5f68565af58fac3efabf8d87084731a3548082d249bebbaea77319fd6010c41ba4bd97370dd833392bbcc22a606ff03dbaad702618a6de541c297548717944acb7bc7ea1919ea648b0902214575cc8eef677319acce07d19e0e95315a11422ce53defc2a6d214875d76b167178559a413a35122fc0c075754ea7b23907f509d49410d56563744c36204c48d258ac6e7df60c7b6716c8528979164c6eb9792f40450a2cacb892234c3b57d5e96e0afb31a4176e57f1e144b6fe67b32a1232bab8cca59441c4245ea24240a36f9cf1569eb43259193cb28c233184929ff262e1af959f6fa79313430da40b8990ef3f141d20f2fa759b60b40c525a229679eafe7851ef0087dee39c18e9c3c5304d1e3916390d03a694f17c1c5c64b93d09a0ad9fb68d66919ada8a2da05c484978f1e52030f494559eb854e49221b4e0c4a7f26268484b71772786df769f43b4c6e54faf2e1638498b60d143ac5504b0e918cce224c2eada56281334d9401f3cfa123490f99bd076ca37cd64b7ac251083fb914f3544dd093044075004e11e8660716b36c9bf7a0f5a2df7ed019a9162cbc332dd78ce35bbf2bf9e472c84c7e03f7e150443ed039715617991b6c4025c715f352fb82585ceb4f1543173c2d2e51a11bc8ada3a74a5df48a46feb526ad8e3a85a3c7ac7d7d5c3b159ef12149543a15abf263fe4c1ae228600cdc1bf1543ebdad7abfd8f4dbc2518d903bacbf37fe3fbfeb447a8b9935889c1e29b13f375e31e814ef3c76f0a2f135e2b0aae79799adcd4e424ecd2fe520188c8f68aaf84a3dc8b9a55cff3f07fd6990ed3fd50600f9fa31cdeb857fd66045c3cfc575729e241680cd3edf1e6f9cb4bf167f83465d5893562fcb168b1c5bd13687bcb30112f696a95171b40f5963c8d58f6279924efd847f2a895cffc4cab5bdc13cfa0c8006c042e95a22c6655f8f328776b')

END
GO
--Check if the Forecasts table is empty, else prefill with some data
IF NOT EXISTS (SELECT * FROM Likes)
BEGIN
	INSERT INTO Likes(ArtID,ArtistID,ArtistName) VALUES
    (1,2,'Sampo'),
	(2,1,'John'),
	(3,2,'Sampo'),
	(4,2,'Sampo'),
	(2,2,'Sampo')
END
GO
IF NOT EXISTS (SELECT * FROM Comment)
BEGIN
	INSERT INTO Comment(ArtID,ArtistID,ArtistName,Comment) VALUES
    (1,2,'Sampo','If you can see this message the comment function works'),
	(2,1,'John','What do you mean your AT Soup'),
	(3,2,'Sampo','If you can see this message the comment function works, on art 3 by Sampo'),
	(2,2,'Sampo','If you can see this message the comment function works on art 2 by Sampo'),
    (1,2,'Sampo','Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Mauris viverra diam vitae quam. Suspendisse potenti. Nullam porttitor lacus at turpis. Donec posuere metus vitae ipsum.'),
    (2,2,'Sampo','"Nulla suscipit ligula in lacus. Curabitur at ipsum ac tellus semper interdum. Mauris ullamcorper purus sit amet nulla. Quisque arcu libero, rutrum ac, lobortis vel, dapibus at, diam. Nam tristique tortor eu pede."'),
    (2,2,'Sampo','"Cras pellentesque volutpat dui. Maecenas tristique, est et tempus semper, est quam pharetra magna, ac consequat metus sapien ut nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Mauris viverra diam vitae quam. Suspendisse potenti. Nullam porttitor lacus at turpis."'),
    (3,2,'Sampo','"Nunc nisl. Duis bibendum, felis sed interdum venenatis, turpis enim blandit mi, in porttitor pede justo eu massa. Donec dapibus."'),
    (1,2,'Sampo','"Duis ac nibh. Fusce lacus purus, aliquet at, feugiat non, pretium quis, lectus. Suspendisse potenti. In eleifend quam a odio. In hac habitasse platea dictumst."'),
    (1,2,'Sampo','"Proin interdum mauris non ligula pellentesque ultrices. Phasellus id sapien in sapien iaculis congue. Vivamus metus arcu, adipiscing molestie, hendrerit at, vulputate vitae, nisl. Aenean lectus."'),
    (1,2,'Sampo','Suspendisse potenti. In eleifend quam a odio.'),
    (2,2,'Sampo','"Phasellus id sapien in sapien iaculis congue. Vivamus metus arcu, adipiscing molestie, hendrerit at, vulputate vitae, nisl."'),
    (2,2,'Sampo','"Quisque erat eros, viverra eget, congue eget, semper rutrum, nulla. Nunc purus. Phasellus in felis. Donec semper sapien a libero."'),
    (1,2,'Sampo','Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Mauris viverra diam vitae quam.'),
    (2,2,'Sampo','Nulla ut erat id mauris vulputate elementum. Nullam varius. Nulla facilisi.'),
    (2,2,'Sampo','Integer ac leo.'),
    (3,2,'Sampo','"Nam congue, risus semper porta volutpat, quam pede lobortis ligula, sit amet eleifend pede libero quis orci. Nullam molestie nibh in lectus. Pellentesque at nulla."'),
    (1,2,'Sampo','"In tempor, turpis nec euismod scelerisque, quam turpis adipiscing lorem, vitae mattis nibh ligula nec sem. Duis aliquam convallis nunc. Proin at turpis a pede posuere nonummy. Integer non velit. Donec diam neque, vestibulum eget, vulputate ut, ultrices vel, augue."'),
    (1,2,'Sampo','"Maecenas tristique, est et tempus semper, est quam pharetra magna, ac consequat metus sapien ut nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Mauris viverra diam vitae quam. Suspendisse potenti."'),
    (1,2,'Sampo','"Maecenas leo odio, condimentum id, luctus nec, molestie sed, justo. Pellentesque viverra pede ac diam. Cras pellentesque volutpat dui."'),
    (2,2,'Sampo','Morbi a ipsum.'),
    (3,2,'Sampo','"Morbi non lectus. Aliquam sit amet diam in magna bibendum imperdiet. Nullam orci pede, venenatis non, sodales sed, tincidunt eu, felis. Fusce posuere felis sed lacus. Morbi sem mauris, laoreet ut, rhoncus aliquet, pulvinar sed, nisl."'),
    (1,2,'Sampo','"Phasellus id sapien in sapien iaculis congue. Vivamus metus arcu, adipiscing molestie, hendrerit at, vulputate vitae, nisl. Aenean lectus. Pellentesque eget nunc. Donec quis orci eget orci vehicula condimentum."'),
    (1,2,'Sampo','In hac habitasse platea dictumst.'),
    (3,2,'Sampo','Etiam justo. Etiam pretium iaculis justo. In hac habitasse platea dictumst. Etiam faucibus cursus urna.'),
    (2,2,'Sampo','Maecenas ut massa quis augue luctus tincidunt. Nulla mollis molestie lorem.'),
    (2,2,'Sampo','Mauris sit amet eros. Suspendisse accumsan tortor quis turpis. Sed ante. Vivamus tortor.'),
    (3,2,'Sampo','"Curabitur convallis. Duis consequat dui nec nisi volutpat eleifend. Donec ut dolor. Morbi vel lectus in quam fringilla rhoncus. Mauris enim leo, rhoncus sed, vestibulum sit amet, cursus id, turpis."'),
    (3,2,'Sampo','"Donec ut mauris eget massa tempor convallis. Nulla neque libero, convallis eget, eleifend luctus, ultricies eu, nibh. Quisque id justo sit amet sapien dignissim vestibulum."'),
    (3,2,'Sampo','"Pellentesque eget nunc. Donec quis orci eget orci vehicula condimentum. Curabitur in libero ut massa volutpat convallis. Morbi odio odio, elementum eu, interdum eu, tincidunt in, leo. Maecenas pulvinar lobortis est."'),
    (2,2,'Sampo','In quis justo. Maecenas rhoncus aliquam lacus.'),
    (2,2,'Sampo','"Fusce lacus purus, aliquet at, feugiat non, pretium quis, lectus."'),
    (1,2,'Sampo','"Morbi sem mauris, laoreet ut, rhoncus aliquet, pulvinar sed, nisl. Nunc rhoncus dui vel sem. Sed sagittis."'),
    (1,2,'Sampo','Duis aliquam convallis nunc. Proin at turpis a pede posuere nonummy. Integer non velit.'),
    (2,2,'Sampo','"Proin eu mi. Nulla ac enim. In tempor, turpis nec euismod scelerisque, quam turpis adipiscing lorem, vitae mattis nibh ligula nec sem. Duis aliquam convallis nunc."'),
    (2,2,'Sampo','Sed ante. Vivamus tortor. Duis mattis egestas metus. Aenean fermentum. Donec ut mauris eget massa tempor convallis.'),
    (3,2,'Sampo','In eleifend quam a odio. In hac habitasse platea dictumst.'),
    (3,2,'Sampo','Suspendisse potenti.'),
    (1,2,'Sampo','"Quisque porta volutpat erat. Quisque erat eros, viverra eget, congue eget, semper rutrum, nulla."'),
    (1,2,'Sampo','"In sagittis dui vel nisl. Duis ac nibh. Fusce lacus purus, aliquet at, feugiat non, pretium quis, lectus."'),
    (1,2,'Sampo','Curabitur convallis. Duis consequat dui nec nisi volutpat eleifend. Donec ut dolor. Morbi vel lectus in quam fringilla rhoncus.'),
    (1,2,'Sampo','"Nulla neque libero, convallis eget, eleifend luctus, ultricies eu, nibh. Quisque id justo sit amet sapien dignissim vestibulum. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nulla dapibus dolor vel est. Donec odio justo, sollicitudin ut, suscipit a, feugiat et, eros."'),
    (1,2,'Sampo','"Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nulla dapibus dolor vel est. Donec odio justo, sollicitudin ut, suscipit a, feugiat et, eros. Vestibulum ac est lacinia nisi venenatis tristique. Fusce congue, diam id ornare imperdiet, sapien urna pretium nisl, ut volutpat sapien arcu sed augue. Aliquam erat volutpat."'),
    (3,2,'Sampo','"Vestibulum sed magna at nunc commodo placerat. Praesent blandit. Nam nulla. Integer pede justo, lacinia eget, tincidunt eget, tempus vel, pede."'),
    (2,2,'Sampo','Praesent id massa id nisl venenatis lacinia. Aenean sit amet justo.'),
    (2,2,'Sampo','"Vivamus metus arcu, adipiscing molestie, hendrerit at, vulputate vitae, nisl. Aenean lectus. Pellentesque eget nunc."'),
    (3,2,'Sampo','Quisque id justo sit amet sapien dignissim vestibulum. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nulla dapibus dolor vel est.'),
    (1,2,'Sampo','"Integer pede justo, lacinia eget, tincidunt eget, tempus vel, pede. Morbi porttitor lorem id ligula. Suspendisse ornare consequat lectus. In est risus, auctor sed, tristique in, tempus sit amet, sem. Fusce consequat."'),
    (1,2,'Sampo','"Cras in purus eu magna vulputate luctus. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vivamus vestibulum sagittis sapien."'),
    (1,2,'Sampo','Suspendisse potenti.'),
    (3,2,'Sampo','"Nunc nisl. Duis bibendum, felis sed interdum venenatis, turpis enim blandit mi, in porttitor pede justo eu massa. Donec dapibus."'),
    (2,2,'Sampo','"Vivamus metus arcu, adipiscing molestie, hendrerit at, vulputate vitae, nisl."'),
    (3,2,'Sampo','"Quisque ut erat. Curabitur gravida nisi at nibh. In hac habitasse platea dictumst. Aliquam augue quam, sollicitudin vitae, consectetuer eget, rutrum at, lorem. Integer tincidunt ante vel ipsum."'),
    (1,2,'Sampo','Maecenas rhoncus aliquam lacus.'),
    (1,2,'Sampo','Sed sagittis.'),
    (1,2,'Sampo','"Aenean fermentum. Donec ut mauris eget massa tempor convallis. Nulla neque libero, convallis eget, eleifend luctus, ultricies eu, nibh. Quisque id justo sit amet sapien dignissim vestibulum. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nulla dapibus dolor vel est."'),
    (2,2,'Sampo','"Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nulla dapibus dolor vel est. Donec odio justo, sollicitudin ut, suscipit a, feugiat et, eros. Vestibulum ac est lacinia nisi venenatis tristique. Fusce congue, diam id ornare imperdiet, sapien urna pretium nisl, ut volutpat sapien arcu sed augue."'),
    (1,2,'Sampo','"Vivamus metus arcu, adipiscing molestie, hendrerit at, vulputate vitae, nisl. Aenean lectus."'),
    (3,2,'Sampo','Maecenas pulvinar lobortis est. Phasellus sit amet erat. Nulla tempus. Vivamus in felis eu sapien cursus vestibulum.'),
    (3,2,'Sampo','"Morbi porttitor lorem id ligula. Suspendisse ornare consequat lectus. In est risus, auctor sed, tristique in, tempus sit amet, sem. Fusce consequat."'),
    (1,2,'Sampo','Vivamus vestibulum sagittis sapien.'),
    (2,2,'Sampo','Nulla ut erat id mauris vulputate elementum. Nullam varius. Nulla facilisi.'),
    (1,2,'Sampo','"Ut at dolor quis odio consequat varius. Integer ac leo. Pellentesque ultrices mattis odio. Donec vitae nisi. Nam ultrices, libero non mattis pulvinar, nulla pede ullamcorper augue, a suscipit nulla elit ac nulla."'),
    (1,2,'Sampo','Vivamus in felis eu sapien cursus vestibulum. Proin eu mi.'),
    (3,2,'Sampo','In hac habitasse platea dictumst.'),
    (2,2,'Sampo','Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Mauris viverra diam vitae quam. Suspendisse potenti. Nullam porttitor lacus at turpis. Donec posuere metus vitae ipsum.'),
    (1,2,'Sampo','Nulla ac enim.'),
    (1,2,'Sampo','"Nulla neque libero, convallis eget, eleifend luctus, ultricies eu, nibh. Quisque id justo sit amet sapien dignissim vestibulum. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nulla dapibus dolor vel est. Donec odio justo, sollicitudin ut, suscipit a, feugiat et, eros."'),
    (3,2,'Sampo','Vivamus tortor.'),
    (1,2,'Sampo','"Aliquam augue quam, sollicitudin vitae, consectetuer eget, rutrum at, lorem. Integer tincidunt ante vel ipsum. Praesent blandit lacinia erat. Vestibulum sed magna at nunc commodo placerat. Praesent blandit."'),
    (1,2,'Sampo','Sed accumsan felis. Ut at dolor quis odio consequat varius.'),
    (2,2,'Sampo','Praesent blandit lacinia erat.'),
    (2,2,'Sampo','"Sed vel enim sit amet nunc viverra dapibus. Nulla suscipit ligula in lacus. Curabitur at ipsum ac tellus semper interdum. Mauris ullamcorper purus sit amet nulla. Quisque arcu libero, rutrum ac, lobortis vel, dapibus at, diam."'),
    (1,2,'Sampo','"Nullam orci pede, venenatis non, sodales sed, tincidunt eu, felis."'),
    (3,2,'Sampo','"Nulla ac enim. In tempor, turpis nec euismod scelerisque, quam turpis adipiscing lorem, vitae mattis nibh ligula nec sem."'),
    (2,2,'Sampo','"Quisque ut erat. Curabitur gravida nisi at nibh. In hac habitasse platea dictumst. Aliquam augue quam, sollicitudin vitae, consectetuer eget, rutrum at, lorem."'),
    (3,2,'Sampo','"In est risus, auctor sed, tristique in, tempus sit amet, sem."'),
    (1,2,'Sampo','Vestibulum rutrum rutrum neque. Aenean auctor gravida sem.'),
    (1,2,'Sampo','In congue. Etiam justo. Etiam pretium iaculis justo. In hac habitasse platea dictumst.'),
    (1,2,'Sampo','"Aenean fermentum. Donec ut mauris eget massa tempor convallis. Nulla neque libero, convallis eget, eleifend luctus, ultricies eu, nibh. Quisque id justo sit amet sapien dignissim vestibulum."'),
    (2,2,'Sampo','"Proin risus. Praesent lectus. Vestibulum quam sapien, varius ut, blandit non, interdum in, ante. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Duis faucibus accumsan odio. Curabitur convallis."'),
    (2,2,'Sampo','Nunc nisl.'),
    (1,2,'Sampo','Vestibulum rutrum rutrum neque. Aenean auctor gravida sem. Praesent id massa id nisl venenatis lacinia.'),
    (3,2,'Sampo','In congue. Etiam justo. Etiam pretium iaculis justo. In hac habitasse platea dictumst.'),
    (3,2,'Sampo','"Fusce lacus purus, aliquet at, feugiat non, pretium quis, lectus. Suspendisse potenti. In eleifend quam a odio. In hac habitasse platea dictumst. Maecenas ut massa quis augue luctus tincidunt."'),
    (2,2,'Sampo','Etiam pretium iaculis justo. In hac habitasse platea dictumst. Etiam faucibus cursus urna.'),
    (1,2,'Sampo','"Nulla suscipit ligula in lacus. Curabitur at ipsum ac tellus semper interdum. Mauris ullamcorper purus sit amet nulla. Quisque arcu libero, rutrum ac, lobortis vel, dapibus at, diam. Nam tristique tortor eu pede."'),
    (2,2,'Sampo','Nullam molestie nibh in lectus. Pellentesque at nulla. Suspendisse potenti.'),
    (2,2,'Sampo','"Curabitur in libero ut massa volutpat convallis. Morbi odio odio, elementum eu, interdum eu, tincidunt in, leo."'),
    (1,2,'Sampo','"Curabitur in libero ut massa volutpat convallis. Morbi odio odio, elementum eu, interdum eu, tincidunt in, leo. Maecenas pulvinar lobortis est. Phasellus sit amet erat."'),
    (3,2,'Sampo','Nulla suscipit ligula in lacus. Curabitur at ipsum ac tellus semper interdum. Mauris ullamcorper purus sit amet nulla.'),
    (3,2,'Sampo','"Vestibulum quam sapien, varius ut, blandit non, interdum in, ante. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Duis faucibus accumsan odio."'),
    (1,2,'Sampo','Quisque id justo sit amet sapien dignissim vestibulum. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nulla dapibus dolor vel est.'),
    (1,2,'Sampo','Curabitur gravida nisi at nibh. In hac habitasse platea dictumst.'),
    (1,2,'Sampo','"Suspendisse ornare consequat lectus. In est risus, auctor sed, tristique in, tempus sit amet, sem. Fusce consequat."'),
    (1,2,'Sampo','Nulla ac enim.'),
    (2,2,'Sampo','Maecenas tincidunt lacus at velit. Vivamus vel nulla eget eros elementum pellentesque.'),
    (3,2,'Sampo','"Proin at turpis a pede posuere nonummy. Integer non velit. Donec diam neque, vestibulum eget, vulputate ut, ultrices vel, augue. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Donec pharetra, magna vestibulum aliquet ultrices, erat tortor sollicitudin mi, sit amet lobortis sapien sapien non mi. Integer ac neque."'),
    (2,2,'Sampo','"Praesent id massa id nisl venenatis lacinia. Aenean sit amet justo. Morbi ut odio. Cras mi pede, malesuada in, imperdiet et, commodo vulputate, justo. In blandit ultrices enim."'),
    (3,2,'Sampo','"Nam congue, risus semper porta volutpat, quam pede lobortis ligula, sit amet eleifend pede libero quis orci."'),
    (2,2,'Sampo','"Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Proin risus."'),
    (3,2,'Sampo','"Morbi vestibulum, velit id pretium iaculis, diam erat fermentum justo, nec condimentum neque sapien placerat ante. Nulla justo. Aliquam quis turpis eget elit sodales scelerisque. Mauris sit amet eros. Suspendisse accumsan tortor quis turpis."'),
    (3,2,'Sampo','"Curabitur gravida nisi at nibh. In hac habitasse platea dictumst. Aliquam augue quam, sollicitudin vitae, consectetuer eget, rutrum at, lorem. Integer tincidunt ante vel ipsum. Praesent blandit lacinia erat."'),
    (2,2,'Sampo','Vestibulum ac est lacinia nisi venenatis tristique.')


END
GO



