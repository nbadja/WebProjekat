import {Prodavnica, preuzmiProdavnice, dodajModel} from "./Prodavnica.js"
import {createDrzaveDropdown, ucitajPrvuDrzavu} from "./Drzava.js"

var container = document.createElement("div");
container.classList.add("container","table-responsive");


var header = document.createElement("div");
header.classList.add("header");
var h1 = document.createElement("h1");
h1.classList.add("col");
h1.innerHTML = "Cenovnik";
header.appendChild(h1);
container.appendChild(header);


createDrzaveDropdown(container);

document.body.appendChild(container);

dodajModel();