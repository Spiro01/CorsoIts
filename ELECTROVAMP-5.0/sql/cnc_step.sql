CREATE TABLE test_table(
    id int AUTO_INCREMENT PRIMARY KEY,
    counter SMALLINT,
    counter_planned SMALLINT,
    counter_scraps SMALLINT,
    status BOOLEAN,
    pnc TEXT,
    cycle_time BIGINT,
    serial_number TEXT
);

INSERT INTO test_table (counter, counter_planned, counter_scraps, status, pnc, cycle_time, serial_number)
VALUES
(5, 120, 8, true, "fdfs4r43ff.edc", 12325, "1j54234huf");

SELECT * FROM test_table ORDER BY id DESC LIMIT 1;

SELECT * FROM test_table;