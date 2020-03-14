import { Component, OnInit } from "@angular/core";
import { ApiService } from "src/app/services/api.service";
import { ICocktailDto } from "src/app/_generated/interfaces";
import { PageLoaderService } from "src/app/services/page-loader.service";
import { eCommandRunType } from "src/app/_generated/enums";

@Component({
  selector: "app-presenter",
  templateUrl: "./presenter.component.html",
  styleUrls: ["./presenter.component.scss"]
})
export class PresenterComponent implements OnInit {
  constructor(private api: ApiService, private loader: PageLoaderService) {}

  dataLoaded = false;
  cocktails: ICocktailDto[];
  savingType: eCommandRunType;
  saveBtnEnabled = false;

  _eConvert = eCommandRunType;

  ngOnInit(): void {
    this.savingType = eCommandRunType.RunOnChange;
    // this.loader.show();
    // this.api.getAllCocktails().subscribe(response => {
    //   this.cocktails = response;
    //   this.dataLoaded = true;
    //   this.loader.hide();
    // });
    this.dataLoaded = true;
  }

  toggleSavingType() {
      if(this.savingType === eCommandRunType.RunOnChange) {
        this.savingType = eCommandRunType.Batch;
        this.saveBtnEnabled = true;
      } else {
        this.savingType = eCommandRunType.RunOnChange;
        this.saveBtnEnabled = false;
      }
  }

  createCocktail() {
      let cocktail = {
          name: 'Bees knees',
          strength: 30,
          ingredients: []
      } as ICocktailDto;

      this.api.createCocktail(cocktail)
        .subscribe(result => {
            console.log(result);
        });
  }
}
