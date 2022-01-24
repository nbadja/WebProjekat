import {Predmet} from "./Predmet.js"

let instanca = 0;
var listaPredmeta = [];

export function resetInstance()
{
    instanca = 0;
}

export class Prodavnica
{
    constructor(ID, Naziv, Drzava, DrzavaID, Grad, Adresa)
    {
        this.ID = ID;
        this.Naziv = Naziv;
        this.Drzava = Drzava;
        this.DrzavaID = DrzavaID;
        this.Grad = Grad;
        this.Adresa = Adresa;
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

        var viewBtn = document.createElement("button");
        viewBtn.innerHTML = "View";
        viewBtn.classList.add("dropdown-item");
        let naziv = this.Naziv;
        var id = this.ID;
        viewBtn.onclick = function()
        {
            preuzmiPredmete(id);
        };

        var editBtn = document.createElement("button");
        editBtn.innerHTML = "Edit";
        editBtn.classList.add("dropdown-item");
        var adresaText = this.Adresa;
        var drzavaID = this.DrzavaID;
        editBtn.onclick = function()
        {
            $(".modal").show();

            $(".modelTabela").get(0).innerHTML = "";

            var div = document.createElement("div");
            div.classList.add("row", "prodavnicaRed", "justify-content-center");

            var name = document.createElement("input");
            name.value = naziv;
            name.classList.add("row");

            var adresa = document.createElement("input");
            adresa.value = adresaText;
            adresa.classList.add("row");

            var btnUpdejt = document.createElement("button");
            btnUpdejt.innerHTML = "Apply";
            btnUpdejt.classList.add("row", "btn", "btn-primary");
            btnUpdejt.onclick = function()
            {
                updateProdavnicu(id, name.value, adresa.value, host, drzavaID);
            };

            div.appendChild(name);
            div.appendChild(adresa);
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
            deleteProdavnica(host, drzavaID, prodId);
        };



        buttonControls.appendChild(viewBtn);
        buttonControls.appendChild(editBtn);
        buttonControls.appendChild(deleteBtn);

        menuBtn.appendChild(buttonControls);
        tr.appendChild(menuBtn);


        var td = document.createElement("div");
        td.classList.add("imgContainer", "col-sm", "h-10");

        var text = document.createElement("span");
        text.style.fontSize = '40';
        text.style.fontWeight = 'bold';
        text.innerHTML = instanca+ 1;

        let img = document.createElement("img");
        img.src = "assets/prodavnica.png";
        img.className = "prodavnicaImg";

        td.appendChild(text);
        td.appendChild(img);
        tr.appendChild(td);

        var text = document.createElement("div");
        text.innerHTML= this.Naziv;
        text.classList.add("col", "h-10");
        tr.appendChild(text);

        var text = document.createElement("div");
        text.innerHTML= this.Drzava;
        text.classList.add("col",  "h-10");
        tr.appendChild(text);

        var text = document.createElement("div");
        text.innerHTML= this.Grad;
        text.classList.add("col", "h-10");
        tr.appendChild(text);

        var text = document.createElement("div");
        text.innerHTML= this.Adresa;
        text.classList.add("col", "h-10");
        tr.appendChild(text);

        tr.classList.add("polje_"+instanca, "mojRed");
        instanca++;
    }
}


function preuzmiPredmete(id)
{
        console.log("https://localhost:5001/Storage/PreuzmiStorage/" + id);

        $(".modal").show();

        fetch("https://localhost:5001/Storage/PreuzmiStorage/" + id,
         {method : 'GET'}
        )
        .then(p => {
            p.json().then(storage => {
                let index = 0;
                $(".modelTabela").get(0).innerHTML = '';

                var div = document.createElement("div");
                div.classList.add("row");


                var containerPredmet = document.createElement("div");
                containerPredmet.classList.add("grad-select","col-10", "col-xs-12");

                var selectPredmet = document.createElement("select");
                selectPredmet.classList.add("predmetDropdown",  "h-100", "w-100");
                containerPredmet.appendChild(selectPredmet);

                fetch("https://localhost:5001/Predmet/PreuzmiPredmet",
                {method : 'GET'}
                )
               .then(p => {
                   p.json().then(predmet => {
                        predmet.forEach(info => {
                            var option = document.createElement('option');
                            option.text = info.naziv + "    Barcode: " + info.barCode + "   Cena:" + info.cena;
                            option.value = info.id;
                            selectPredmet.add(option);
                        })
                    })
                }
                );
                div.appendChild(containerPredmet);

                var btnDiv = document.createElement("div");
                btnDiv.classList.add("col");
                var th = document.createElement("button");
                th.classList.add("w-100");
                th.innerHTML = "Add";
                th.onclick = function()
                {
                    dodajPredmet(id);
                };
                btnDiv.appendChild(th);
                div.appendChild(btnDiv);

                $(".modelTabela").get(0).appendChild(div);

                storage.forEach(predmet => {
                    index++;
                    var p  = new Predmet(predmet.predmet.id, predmet.predmet.naziv, predmet.predmet.barCode, predmet.predmet.cena);
                    listaPredmeta.push(p);
                    var tr = document.createElement("div");
                    tr.classList.add("row",  "justify-content-center", "prodavnicaRed", "polje");

                    var text = document.createElement("div");
                    text.classList.add("col");
                    text.innerHTML= index;
                    tr.appendChild(text);

                    var text = document.createElement("div");
                    text.classList.add("col");
                    text.innerHTML= predmet.predmet.naziv;
                    tr.appendChild(text);

                    var text = document.createElement("div");
                    text.classList.add("col");
                    text.innerHTML= predmet.predmet.barCode;
                    tr.appendChild(text);

                    var text = document.createElement("div");
                    text.classList.add("col");
                    text.innerHTML= predmet.predmet.cena;
                    tr.appendChild(text);

                    var buttonControls = document.createElement("div");
                    buttonControls.classList.add("col");

                    var deleteBtn = document.createElement("button");
                    deleteBtn.classList.add("btn", "btn-primary", "w-100");
                    deleteBtn.innerHTML = "Delete";
                    deleteBtn.onclick = function()
                    {
                        deletePredmeti(id, predmet.predmet.id);
                    };

                    buttonControls.appendChild(deleteBtn);

                    tr.appendChild(buttonControls);

                    $(".modelTabela").get(0).appendChild(tr);
                });
            })
        });
}


function deletePredmeti(prodavnicaID, predmetID)
{
    console.log("https://localhost:5001/Storage/DeleteStorage/" + prodavnicaID + "/" + predmetID);
    fetch("https://localhost:5001/Storage/DeleteStorage/" + prodavnicaID + "/" + predmetID,
    {method : 'DELETE'}
    )
   .then(p => {
        preuzmiPredmete(prodavnicaID);
    });
}


export function preuzmiProdavnice(div, id)
{
    fetch("https://localhost:5001/Prodavnica/PreuzmiProdavniceDrzava/" + id,
    {method : 'GET'}
    )
    .then(p => {
        p.json().then(prodavnica => {
            div.innerHTML = "";
            prodavnica.forEach(prod => {
                var p  = new Prodavnica(prod.id, prod.naziv, prod.drzava.naziv, prod.drzava.id, prod.grad.naziv, prod.adresa);
                p.popuniTabelu(div);
                console.log("PROD");
            });
            console.log(p);
        })
    })
}


function deleteProdavnica(div, drzava, ID)
{
    console.log("https://localhost:5001/Prodavnica/DeleteProdavnica/" + ID);
    fetch("https://localhost:5001/Prodavnica/DeleteProdavnica/" + ID,
    {method : 'DELETE'}
    )
   .then(p => {
        fetch("https://localhost:5001/Storage/DeleteAllFromStorage/" + ID,
        {method : 'DELETE'}
        )
        .then(p => {
                preuzmiProdavnice(div, drzava);
            });
    });
    instanca = 0;
}


export function dodajProdavnicu(div, Naziv, Drzava, Grad, Adresa)
{
    console.log("https://localhost:5001/Prodavnica/DodajProdavnicu/" + Naziv + "/" + Drzava + "/" + Grad + "/" + Adresa);
    fetch("https://localhost:5001/Prodavnica/DodajProdavnicu/" +  Naziv + "/" + Drzava + "/" + Grad + "/" + Adresa,
    {method : 'POST'}
    )
   .then(p => {
        instanca = 0;
        preuzmiProdavnice(div, Drzava);
    });
}


export function updateProdavnicu(ProdavnicaID, Naziv, Adresa, div, DrzavaID)
{
    console.log("https://localhost:5001/Prodavnica/UpdateProdavnica/" + ProdavnicaID + "/" + Naziv + "/" + Adresa + "/");
    fetch("https://localhost:5001/Prodavnica/UpdateProdavnica/" + ProdavnicaID + "/" + Naziv + "/" + Adresa + "/",
    {method : 'PUT'}
    )
   .then(p => {
        instanca = 0;
        preuzmiProdavnice(div, DrzavaID);
    });
}


export function dodajPredmet(ProdavnicaID)
{
    fetch("https://localhost:5001/Storage/DodajStoregu/" + ProdavnicaID + "/" + $(".predmetDropdown option:selected").val(),
    {method : 'POST'}
    ).then(p => {
        preuzmiPredmete(ProdavnicaID);
    });
}


export function dodajModel()
{
    var model = document.createElement("div");
    model.className = "modal";

    var content = document.createElement("div");
    content.className = "modal-content";

    var close = document.createElement("span");
    close.className = "close";
    close.innerHTML = "&times;";
    close.onclick = function()
    {
        model.style.display = "none";
    }

    var div = document.createElement("div");
    div.classList.add("container", "tableContent", "modelTabela");

    content.appendChild(close);
    content.appendChild(div);

    model.appendChild(content);

    document.body.appendChild(model);
}