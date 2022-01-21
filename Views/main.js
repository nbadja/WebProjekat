import {Prodavnica, preuzmiProdavnice, dodajModel} from "./Prodavnica.js"

var container = document.createElement("div");
container.classList.add("container-fluid","table-responsive");
var div = document.createElement("table");
div.classList.add("flex", "tableContent", "table-striped");
div.id = "mojaTabela";
container.appendChild(div);

document.body.appendChild(container);

dodajModel();
preuzmiProdavnice();