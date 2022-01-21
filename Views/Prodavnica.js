import {Lokacija} from "./Lokacija.js"
import {Predmet} from "./Predmet.js"

let instanca = 0;
var listaPredmeta = [];

export class Prodavnica
{
    constructor(Naziv, Mesto, Ulica)
    {
        this.Naziv = Naziv;
        this.Mesto = Mesto;
        this.Ulica = Ulica;
    }

    popuniTabelu(host)
    {
        var tr = document.createElement("tr");
        host.appendChild(tr);

        var td = document.createElement("td");
        td.className = "imgContainer";

        var text = document.createElement("span");
        text.style.fontSize = '40';
        text.style.fontWeight = 'bold';
        text.innerHTML = instanca+ 1;

        let img = document.createElement("img");
        img.src = "assets/prodavnica.png";
        img.style.height = '50px';
        img.style.width = '50px';
        img.className = "prodavnicaImg";

        td.appendChild(text);
        td.appendChild(img);
        tr.appendChild(td);


        var text = document.createElement("td");
        text.innerHTML= this.Naziv;
        tr.appendChild(text);

        var text = document.createElement("td");
        text.innerHTML= this.Mesto;
        tr.appendChild(text);

        var text = document.createElement("td");
        text.innerHTML= this.Ulica;
        tr.appendChild(text);


        var buttonControls = document.createElement("td");
        buttonControls.className = "btnControls";


        var inputField = document.createElement("input");
        inputField.style.display = "none";
        inputField.style.paddingRight = '20px';
        inputField.placeholder = "Novi Naziv";
        buttonControls.appendChild(inputField);

        var buttonApply = document.createElement("button");
        buttonApply.style.display = "none";
        buttonApply.innerHTML = "Apply";
        buttonApply.onclick = function()
        {
            inputField.style.display = "none";
            buttonApply.style.display = "none";
            if(inputField.value != "")
                updateProdavnicu(naziv, inputField.value);
        };
        buttonControls.appendChild(buttonApply);


        var viewBtn = document.createElement("button");
        viewBtn.innerHTML = "View";
        let naziv = this.Naziv;
        viewBtn.onclick = function()
        {
            preuzmiPredmete(naziv);
        };

        var editBtn = document.createElement("button");
        editBtn.innerHTML = "Edit";
        editBtn.onclick = function()
        {
            inputField.style.display = "";
            buttonApply.style.display = "";
        };

        var deleteBtn = document.createElement("button");
        deleteBtn.innerHTML = "Delete";
        deleteBtn.onclick = function()
        {
            deleteProdavnica(naziv);
        };

        buttonControls.appendChild(viewBtn);
        buttonControls.appendChild(editBtn);
        buttonControls.appendChild(deleteBtn);

        tr.appendChild(buttonControls);

        tr.className = "polje_"+instanca;
        instanca++;
    }
}



function preuzmiPredmete(naziv)
{
        console.log("https://localhost:5001/Storage/PreuzmiStorage/" + naziv);

        var modal = document.getElementById("myModal");
        modal.style.display = "block";

        fetch("https://localhost:5001/Storage/PreuzmiStorage/" + naziv,
         {method : 'GET'}
        )
        .then(p => {
            p.json().then(storage => {
                let index = 0;
                var modal = document.getElementById("modelTabela");
                modal.innerHTML = '';
                var tr = document.createElement("tr");

                var th = document.createElement("th");
                th.innerHTML = "Index";
                tr.appendChild(th);

                var th = document.createElement("th");
                th.innerHTML = "Naziv";
                tr.appendChild(th);

                var th = document.createElement("th");
                th.innerHTML = "BarCode";
                tr.appendChild(th);

                var th = document.createElement("th");
                th.innerHTML = "Cena";
                tr.appendChild(th);

                modal.appendChild(tr);

                var tr = document.createElement("tr");

                tr.appendChild(document.createElement("th"));

                var th = document.createElement("th");
                var input = document.createElement("input");
                input.id = "nazivPredmetaField";
                input.placeholder = "Naziv";
                th.appendChild(input);
                tr.appendChild(th);

                var th = document.createElement("th");
                var input = document.createElement("input");
                input.id = "barField";
                input.placeholder = "BarCode";
                th.appendChild(input);
                tr.appendChild(th);

                var th = document.createElement("th");
                var input = document.createElement("input");
                input.id = "cenaField";
                input.placeholder = "Cena";
                th.appendChild(input);
                tr.appendChild(th);

                var th = document.createElement("button");
                th.className = "btnControls";
                th.innerHTML = "Add";

                th.onclick = function()
                {
                    let predmet = document.getElementById("nazivPredmetaField").value;
                    let bar = document.getElementById("barField").value;
                    let cena = document.getElementById("cenaField").value;
                    dodajPredmet(naziv, predmet, bar, cena);
                };
                tr.appendChild(th);

                modal.appendChild(tr);



                storage.forEach(predmet => {
                    index++;
                    var p  = new Predmet(predmet.predmet.naziv, predmet.predmet.barCode, predmet.predmet.cena);
                    listaPredmeta.push(p);
                    var tr = document.createElement("tr");

                    var text = document.createElement("td");
                    text.innerHTML= index;
                    tr.appendChild(text);

                    var text = document.createElement("td");
                    text.innerHTML= predmet.predmet.naziv;
                    tr.appendChild(text);

                    var text = document.createElement("td");
                    text.innerHTML= predmet.predmet.barCode;
                    tr.appendChild(text);

                    var text = document.createElement("td");
                    text.innerHTML= predmet.predmet.cena;
                    tr.appendChild(text);

                    var buttonControls = document.createElement("td");
                    buttonControls.className = "btnControls";

                    var barcode = document.createElement("input");
                    barcode.style.display = "none";
                    barcode.placeholder = "Barcode";
                    buttonControls.appendChild(barcode);

                    var cena = document.createElement("input");
                    cena.style.display = "none";
                    cena.style.paddingRight = '20px';
                    cena.placeholder = "Cena";
                    buttonControls.appendChild(cena);

                    var buttonApply = document.createElement("button");
                    buttonApply.style.display = "none";
                    buttonApply.innerHTML = "Apply";
                    buttonApply.onclick = function()
                    {
                        cena.style.display = "none";
                        barcode.style.display = "none";
                        buttonApply.style.display = "none";
                        if(cena.value != "" && barcode.value != "")
                            updatePredmet(naziv, predmet.predmet.naziv, barcode.value, cena.value);
                    };
                    buttonControls.appendChild(buttonApply);


                    var editBtn = document.createElement("button");
                    editBtn.innerHTML = "Edit";
                    editBtn.onclick = function()
                    {
                            cena.style.display = "";
                            barcode.style.display = "";
                            buttonApply.style.display = "";
                    };

                    var deleteBtn = document.createElement("button");
                    deleteBtn.innerHTML = "Delete";
                    deleteBtn.onclick = function()
                    {
                        deletePredmeti(naziv,  predmet.predmet.naziv);
                    };


                    buttonControls.appendChild(editBtn);
                    buttonControls.appendChild(deleteBtn);

                    tr.appendChild(buttonControls);

                    modal.appendChild(tr);
                });
            })
        });
}



function dodajHeaderUTabelu(div)
{
    var tr = document.createElement("tr");

    var th = document.createElement("th");
    th.innerHTML = "Index";
    tr.appendChild(th);

    var th = document.createElement("th");
    th.innerHTML = "Naziv";
    tr.appendChild(th);

    var th = document.createElement("th");
    th.innerHTML = "Grad";
    tr.appendChild(th);

    var th = document.createElement("th");
    th.innerHTML = "Ulica";
    tr.appendChild(th);

    div.appendChild(tr);

    var tr = document.createElement("tr");

    tr.appendChild(document.createElement("th"));

    var th = document.createElement("th");
    var input = document.createElement("input");
    input.id = "nazivField";
    input.placeholder = "Naziv";
    th.appendChild(input);
    tr.appendChild(th);

    var th = document.createElement("th");
    var input = document.createElement("input");
    input.id = "gradField";
    input.placeholder = "Grad";
    th.appendChild(input);
    tr.appendChild(th);

    var th = document.createElement("th");
    var input = document.createElement("input");
    input.id = "ulicaField";
    input.placeholder = "Ulica";
    th.appendChild(input);
    tr.appendChild(th);

    var th = document.createElement("button");
    th.className = "btnControls";
    th.innerHTML = "Add";

    th.onclick = function()
    {
        let naziv = document.getElementById("nazivField").value;
        let mesto = document.getElementById("gradField").value;
        let ulica = document.getElementById("ulicaField").value;
        dodajProdavnicu(naziv, mesto, ulica);
    };
    tr.appendChild(th);

    div.appendChild(tr);
}


function deletePredmeti(Naziv, Predmet)
{
    console.log("https://localhost:5001/Storage/DeleteStorage/" + Naziv + "/" + Predmet);
    fetch("https://localhost:5001/Storage/DeleteStorage/" + Naziv + "/" + Predmet,
    {method : 'DELETE'}
    )
   .then(p => {
        preuzmiPredmete(Naziv);
    });
}


export function preuzmiProdavnice()
{
    fetch("https://localhost:5001/Prodavnica/PreuzmiProdavnice",
    {method : 'GET'}
    )
    .then(p => {
        p.json().then(prodavnica => {
            var div = document.getElementById("mojaTabela");
            div.innerHTML = "";

            dodajHeaderUTabelu(div);

            prodavnica.forEach(prod => {
                var p  = new Prodavnica(prod.naziv, prod.mesto.naziv, prod.mesto.ulica);
                p.popuniTabelu(div);
            });
            document.body.appendChild(div);
            console.log(p);
        })
    })
}

function deleteProdavnica(Naziv)
{
    console.log("https://localhost:5001/Prodavnica/DeleteProdavnica/" + Naziv);
    fetch("https://localhost:5001/Prodavnica/DeleteProdavnica/" + Naziv,
    {method : 'DELETE'}
    )
   .then(p => {
        fetch("https://localhost:5001/Storage/DeleteAllFromStorage/" + Naziv,
        {method : 'DELETE'}
        )
        .then(p => {
                preuzmiProdavnice();
            });
    });
    instanca = 0;
}


export function dodajProdavnicu(Naziv, Mesto, Ulica)
{
    console.log("https://localhost:5001/Prodavnica/DodajProdavnicu/" + Naziv + "/" + Mesto + "/" + Ulica);
    fetch("https://localhost:5001/Prodavnica/DodajProdavnicu/" +  Naziv + "/" + Mesto + "/" + Ulica,
    {method : 'POST'}
    )
   .then(p => {
        instanca = 0;
        preuzmiProdavnice();
    });
}


export function updateProdavnicu(Naziv,Novi)
{
    console.log("https://localhost:5001/Prodavnica/UpdateProdavnica/" + Naziv + "/" + Novi + "/");
    fetch("https://localhost:5001/Prodavnica/UpdateProdavnica/" +  Naziv + "/" + Novi + "/",
    {method : 'PUT'}
    )
   .then(p => {
        instanca = 0;
        preuzmiProdavnice();
    });
}

export function updatePredmet(prodavnica, naziv, barcode ,value)
{
    console.log("https://localhost:5001/Predmet/UpdatePredmet/" + naziv + "/" + barcode + "/" + value);
    fetch("https://localhost:5001/Predmet/UpdatePredmet/" +  naziv + "/" + barcode + "/" + value,
    {method : 'PUT'}
    )
   .then(p => {
        preuzmiPredmete(prodavnica);
    });
}


export function dodajPredmet(Prodavnica, Naziv, BarCode, Cena)
{
    console.log("https://localhost:5001/Predmet/DodajPredmet/" + Naziv + "/" + BarCode + "/" + Cena);
    fetch("https://localhost:5001/Predmet/DodajPredmet/" +   Naziv + "/" + BarCode + "/" + Cena,
    {method : 'POST'}
    )
   .then(p => {
        fetch("https://localhost:5001/Storage/DodajStoregu/" + Prodavnica + "/" + Naziv,
        {method : 'POST'}
        ).then(p => {
            preuzmiPredmete(Prodavnica);
        })
    });
}


export function dodajModel()
{
    var model = document.createElement("div");
    model.className = "modal";
    model.id = "myModal";

    var content = document.createElement("div");
    content.className = "modal-content";

    var close = document.createElement("span");
    close.className = "close";
    close.innerHTML = "&times;";
    close.onclick = function()
    {
        model.style.display = "none";
    }

    var div = document.createElement("table");
    div.classList.add("flex", "tableContent",);
    div.id = "modelTabela";

    content.appendChild(close);
    content.appendChild(div);

    model.appendChild(content);

    document.body.appendChild(model);
}