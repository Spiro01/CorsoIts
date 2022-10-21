
const fs = require("fs");
const path = require("path");

const data = [];

const main = async () => {
  const csv = fs.readFileSync(path.join(__dirname, "qzer.CSV")).toString();

  const csvRow = csv.split("\n");

  csvRow.forEach((row) => {
    if (row.length <= 0) return;
    const splittedRow = row.split(";");
    data.push(new Data(splittedRow));
  });
};

main();

class Data {
  constructor(
    Id,
    Cognome,
    Nome,
    Sesso,
    Segno,
    Peso,
    Altezza,
    DataNascita,
    NazioneNascita,
    CittàNascita,
    Città,
    TitoloStudio,
    Html,
    Css,
    Php,
    C,
    Sql,
    Js,
    Sport,
    Cantante,
    Facebook,
    Twitter,
    Instagram,
  ) {
    this.Id = Id;
    this.Cognome = Cognome;
    this.Nome = Nome;
    this.Sesso = Sesso;
    this.Segno = Segno;
    this.Peso = Peso;
    this.Altezza = Altezza;
    this.DataNascita = DataNascita;
    this.NazioneNascita = NazioneNascita;
    this.CittàNascita = CittàNascita;
    this.Città = Città;
    this.TitoloStudio = TitoloStudio;
    this.Html = Html;
    this.Css = Css;
    this.Php = Php;
    this.C = C;
    this.Sql = Sql;
    this.Js = Js;
    this.Sport = Sport;
    this.Cantante = Cantante;
    this.Facebook = Facebook;
    this.Twitter = Twitter;
    this.Instagram = Instagram;
  }
  Id;
  Cognome;
  Nome;
  Sesso;
  Segno;
  Peso;
  Altezza;
  DataNascita;
  NazioneNascita;
  CittàNascita;
  Città;
  TitoloStudio;
  Html;
  Css;
  Php;
  C;
  Sql;
  Js;
  Sport;
  Cantante;
  Facebook;
  Twitter;
  Instagram;
}
