export class Predmet
{
    constructor(ID, Naziv, Barcode, Cena)
    {
        this.ID = ID;
        this.Naziv = Naziv;
        this.Barcode = Barcode;
        this.Cena = Cena;
    }
    popuniTabelu(host)
    {
        var tr = document.createElement("div");
        tr.classList.add("row", "justify-content-center", "prodavnicaRed");
        host.appendChild(tr);


        var menuBtn = document.createElement("button");
        menuBtn.classList.add("menuBtn", "col-1", "h-10", "btn" ,"btn-secondary", "dropdown-toggle");
        menuBtn.innerHTML = "≡";

        var buttonControls = document.createElement("div");
        buttonControls.classList.add("btnControls","col-2", "dropdown-menu");

        var editBtn = document.createElement("button");
        editBtn.innerHTML = "Edit";
        editBtn.classList.add("dropdown-item");
        var id = this.ID;
        var naziv = this.Naziv;
        var barcode = this.Barcode;
        var cenaVal = this.Cena;
        editBtn.onclick = function()
        {
            $(".modal").show();

            $(".modelTabela").get(0).innerHTML = "";

            var div = document.createElement("div");
            div.classList.add("row", "prodavnicaRed", "justify-content-center");

            var name = document.createElement("input");
            name.value = naziv;
            name.classList.add("row");

            var bar = document.createElement("input");
            bar.value = barcode;
            bar.classList.add("row");

            var cena = document.createElement("input");
            cena.value = cenaVal;
            cena.classList.add("row");

            var btnUpdejt = document.createElement("button");
            btnUpdejt.innerHTML = "Apply";
            btnUpdejt.classList.add("row", "btn", "btn-primary");
            btnUpdejt.onclick = function()
            {
                updatePredmet(id, name.value, bar.value, cena.value, host);
            };

            div.appendChild(name);
            div.appendChild(bar);
            div.appendChild(cena);
            div.appendChild(btnUpdejt);

            $(".modelTabela").get(0).appendChild(div);
        };

        var deleteBtn = document.createElement("button");
        deleteBtn.innerHTML = "Delete";
        deleteBtn.classList.add("dropdown-item");
        var drzavaID = this.DrzavaID;
        var prodId = this.ID;
        deleteBtn.onclick = function()
        {
            deletePredmet(id, host);
        };

        buttonControls.appendChild(editBtn);
        buttonControls.appendChild(deleteBtn);

        menuBtn.appendChild(buttonControls);
        tr.appendChild(menuBtn);

        var text = document.createElement("div");
        text.innerHTML= this.Naziv;
        text.classList.add("col", "h-10");
        tr.appendChild(text);

        var text = document.createElement("div");
        text.innerHTML= this.Barcode;
        text.classList.add("col",  "h-10");
        tr.appendChild(text);

        var text = document.createElement("div");
        text.innerHTML= this.Cena;
        text.classList.add("col", "h-10");
        tr.appendChild(text);
    }
}


export function predmetTabela(container)
{
    createTabelaZaPredmete(container);
}

function createTabelaZaPredmete(container)
{
    var host = document.createElement("div");
    host.className = "spisakPredmeta";
    host.innerHTML = "";
    var fields = document.createElement("div");
    fields.className = "tabelaPredmeta";
    dodajHeaderUTabelu(host,fields);
    host.appendChild(fields);
    container.appendChild(host);
    preuzmiPredmete(fields);
}

function dodajHeaderUTabelu(div, tabela)
{
    var parent = document.createElement("div");
    parent.classList.add("row", "justify-content-left", "prodavnicaRed", "prviRed");

    var secondRow = document.createElement("div");
    secondRow.classList.add("row", "justify-content-left", "prodavnicaRed", "drugiRed");

    var naziv = document.createElement("input");
    naziv.classList.add("nazivField", "col-12");
    naziv.placeholder = "Naziv";
    parent.appendChild(naziv);


    var bar = document.createElement("input");
    bar.classList.add("ulicaField","col-6");
    bar.placeholder = "Barcode";
    secondRow.appendChild(bar);


    var cena = document.createElement("input");
    cena.classList.add("ulicaField","col-6");
    cena.placeholder = "Cena";
    secondRow.appendChild(cena);

    var cetvrtiRed = document.createElement("div");
    cetvrtiRed.classList.add("row", "justify-content-center", "prodavnicaRed", "cetvrtiRed");

    var th = document.createElement("button");
    th.classList.add("btn", "btn-primary","col-md-5", "col-xs-12", "col-sm-12");
    th.innerHTML = "Add";

    th.onclick = function()
    {
        dodajPredmet(tabela, naziv.value , bar.value, cena.value);
        naziv.value = "";
        bar.value = "";
        cena.value = "";
    };
    cetvrtiRed.appendChild(th);

    div.appendChild(parent);
    div.appendChild(secondRow);
    div.appendChild(cetvrtiRed);
}



function preuzmiPredmete(div)
{
    fetch("https://localhost:5001/Predmet/PreuzmiPredmet/",
    {method : 'GET'}
    )
    .then(p => {
        p.json().then(prodavnica => {
            div.innerHTML = "";
            prodavnica.forEach(prod => {
                var p  = new Predmet(prod.id, prod.naziv, prod.barCode, prod.cena);
                p.popuniTabelu(div);
                console.log("Predmet");
            });
            console.log(p);
        })
    })
}


export function updatePredmet(id, naziv, barcode , cena, div)
{
    console.log("https://localhost:5001/Predmet/UpdatePredmet/" + id + "/" + naziv + "/" + barcode + "/" + cena);
    fetch("https://localhost:5001/Predmet/UpdatePredmet/" +  id + "/" + naziv + "/" + barcode + "/" + cena,
    {method : 'PUT'}
    )
   .then(p => {
        preuzmiPredmete(div);
    });
}


export function dodajPredmet(div, naziv, barcode , cena)
{
    console.log("https://localhost:5001/Predmet/DodajPredmet/" + naziv + "/" + barcode + "/" + cena);
    fetch("https://localhost:5001/Predmet/DodajPredmet/" +  naziv + "/" + barcode + "/" + cena,
    {method : 'POST'}
    )
   .then(p => {
        preuzmiPredmete(div);
    });
}


export function deletePredmet(id, div)
{
    console.log("https://localhost:5001/Predmet/DeletePredmet/" + id);
    fetch("https://localhost:5001/Predmet/DeletePredmet/" +  id,
    {method : 'DELETE'}
    )
   .then(p => {
        preuzmiPredmete(div);
    });
}