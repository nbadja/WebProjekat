import {preuzmiProdavnice, dodajProdavnicu, resetInstance} from './Prodavnica.js'
import {createGradDropdown, preuzmiGradove} from './Grad.js'
import {predmetTabela} from './Predmet.js'

export var drzave = [];

export class Drzava
{
    constructor(ID, Naziv)
    {
        this.ID = ID;
        this.Naziv = Naziv;
    }

    createInstance(container)
    {
        var host = document.createElement("div");
        host.className = "spisakProdavnica";
        host.innerHTML = "";
        var fields = document.createElement("div");
        fields.className = "tabelaProdavnica";
        this.dodajHeaderUTabelu(host,fields);
        host.appendChild(fields);
        container.appendChild(host);
        preuzmiProdavnice(fields, this.ID);
    }

    dodajHeaderUTabelu(div, tabela)
    {
        var parent = document.createElement("div");
        parent.classList.add("row", "justify-content-left", "prodavnicaRed", "prviRed");

        var text = document.createElement("div");
        text.innerHTML= "Drzava: " + this.Naziv;
        text.className = "col";
        parent.appendChild(text);

        var secondRow = document.createElement("div");
        secondRow.classList.add("row", "justify-content-left", "prodavnicaRed", "drugiRed");

        var nazivRadnje = document.createElement("input");
        nazivRadnje.classList.add("nazivField", "col-12");
        nazivRadnje.placeholder = "Naziv";
        secondRow.appendChild(nazivRadnje);



        var three = document.createElement("div");
        three.classList.add("row", "justify-content-left", "prodavnicaRed", "treciRed");

        var adresa = document.createElement("input");
        adresa.classList.add("ulicaField","col-6");
        adresa.placeholder = "Ulica";
        three.appendChild(adresa);

        var containerGrad = document.createElement("div");
        containerGrad.classList.add("grad-select","col-6");

        var selectDrzava = document.createElement("select");
        selectDrzava.name = "gradovi";
        selectDrzava.classList.add("gradoviDropdown",  "h-100", "w-100");
        containerGrad.appendChild(selectDrzava);
        preuzmiGradove(selectDrzava);

        three.appendChild(containerGrad);

        var cetvrtiRed = document.createElement("div");
        cetvrtiRed.classList.add("row", "justify-content-center", "prodavnicaRed", "cetvrtiRed");

        var th = document.createElement("button");
        th.classList.add("btn", "btn-primary","col-md-5", "col-xs-12", "col-sm-12");
        th.innerHTML = "Add";
        var id = this.ID;
        th.onclick = function()
        {
            dodajProdavnicu(tabela, nazivRadnje.value, id , selectDrzava.options[selectDrzava.selectedIndex].value,adresa.value);
        };
        cetvrtiRed.appendChild(th);

        div.appendChild(parent);
        div.appendChild(secondRow);
        div.appendChild(three);
        div.appendChild(cetvrtiRed);
    }
}



function preuzmiDrzave(dropDown)
{
    fetch("https://localhost:5001/Drzava/PreuzmiDrzave",
    {method : 'GET'}
    )
    .then(p => {
        if(p.ok)
        {
            p.json().then(drzava => {
                dropDown.innerHTML = "";

                drzava.forEach(infoDrzava => {
                    var option = document.createElement('option');
                    option.text = infoDrzava.naziv;
                    option.value = infoDrzava.id;
                    dropDown.add(option);
                });
            })
        }
        else
        {
                var txt = p.text().then(message => {
                    alert(message);
                });
        }

    })
}


export function createDrzaveDropdown(container)
{
    var red = document.createElement("div");
    red.classList.add("row", "justify-content-left", "prodavnicaRed");

    var containerDrzave = document.createElement("div");
    containerDrzave.classList.add("drzave-select", "col-md-4", "col-sm-12", "col-xs-12");

    var selectDrzava = document.createElement("select");
    selectDrzava.name = "drzave";
    selectDrzava.classList.add("drzaveDropdown", "w-100");
    selectDrzava.selectedIndex = 0;
    containerDrzave.appendChild(selectDrzava);
    preuzmiDrzave(selectDrzava);


    var dodajDrzavu = document.createElement("button");
    dodajDrzavu.innerHTML = "+";
    dodajDrzavu.classList.add("btn", "btn-primary", "ml-10", "col-md-2", "col-xs-12", "col-sm-12");
    dodajDrzavu.onclick = function()
    {
        var drzava = new Drzava($(".drzaveDropdown option:selected").val(), $(".drzaveDropdown option:selected").text());
        var alreadyAdded = false;
        drzave.forEach(drzavaInfo => {
            if(drzavaInfo.ID === drzava.ID)
            {
                alert("Instanca izabrane drzave je vec dodata");
                alreadyAdded = true;
            }
        });
        if(alreadyAdded == false)
        {
            drzave.push(drzava);
            container.appendChild(document.createElement("br"));
            drzava.createInstance(container);
            container.appendChild(document.createElement("br"));
            container.appendChild(document.createElement("br"));
            resetInstance();
            if($(".drzaveDropdown")[0].length == 0)
            {
                var option = document.createElement("option");
                option.text = "PRAZNO";
                $(".drzaveDropdown").get(0).add(option);
                $(".drzaveDropdown").prop("disabled", true );
                dodajDrzavu.disabled = true;
            }
        }
    };
    red.appendChild(containerDrzave);
    red.appendChild(dodajDrzavu);

    var predmeti = document.createElement("div");
    predmeti.classList.add("col");

    var btn = document.createElement("button");
    btn.classList.add("btn", "btn-primary", "w-100", "predmetiBtn");
    btn.innerHTML = "Predmeti";
    btn.onclick = function()
    {
        btn.disabled = true;
        predmetTabela(container);
    };
    predmeti.appendChild(btn);
    red.appendChild(predmeti);

    container.appendChild(red);
}

export function ucitajPrvuDrzavu(container)
{
    var drzava = new Drzava($(".drzaveDropdown option:selected").val(), $(".drzaveDropdown option:selected").text());
    drzave.push(drzava);
    drzava.createInstance(container);
}


