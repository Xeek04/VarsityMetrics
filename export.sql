BEGIN TRANSACTION;
INSERT INTO "Accounts" VALUES (1,'b','b','b',0);
INSERT INTO "Footage" VALUES (1,'https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/GoogleIO-2014-CastingToTheFuture.mp4',1,NULL);
INSERT INTO "Footage" VALUES (2,'https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4',2,NULL);
INSERT INTO "Footage" VALUES (3,'https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/GoogleIO-2014-MakingGoogleCastReadyAppsDiscoverable.mp4',3,NULL);
INSERT INTO "Footage" VALUES (4,'https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4',12,NULL);
INSERT INTO "Game" VALUES (1,'Ruston High','2013-03-14',14,13);
INSERT INTO "Game" VALUES (2,'STM','2007-12-25',500,3);
INSERT INTO "Game" VALUES (3,'Los Angeles Rams','2025-03-01',24,30);
INSERT INTO "Game" VALUES (4,'Green Bay Packers','2025-03-08',17,14);
INSERT INTO "Game" VALUES (5,'Chicago Bears','2025-03-15',31,28);
INSERT INTO "Game" VALUES (6,'Detroit Lions','2025-03-22',10,21);
INSERT INTO "Game" VALUES (7,'San Francisco 49ers','2025-03-29',27,34);
INSERT INTO "Game" VALUES (8,'Seattle Seahawks','2025-04-05',14,10);
INSERT INTO "Game" VALUES (9,'New Orleans Saints','2025-04-12',21,24);
INSERT INTO "Game" VALUES (10,'Tampa Bay Buccaneers','2025-04-19',28,31);
INSERT INTO "Game" VALUES (11,'Dallas Cowboys','2025-04-26',35,27);
INSERT INTO "Game" VALUES (12,'Philadelphia Eagles','2025-05-03',17,20);
COMMIT;
