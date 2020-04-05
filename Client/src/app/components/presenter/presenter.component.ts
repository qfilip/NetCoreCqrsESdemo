import { Component, OnInit, ViewChild } from "@angular/core";
import { ApiService } from "src/app/services/api.service";
import { ICocktailDto } from "src/app/_generated/interfaces";
import { PageLoaderService } from "src/app/services/page-loader.service";
import { eSaveChangeType } from "src/app/_generated/enums";
import { EventPanelComponent } from './event-panel/event-panel.component';

@Component({
  selector: "app-presenter",
  templateUrl: "./presenter.component.html",
  styleUrls: ["./presenter.component.scss"]
})
export class PresenterComponent implements OnInit {
  dataLoaded = false;
  cocktails: ICocktailDto[];
  savingType: eSaveChangeType;
  saveBtnEnabled = false;

  _eConvert = eSaveChangeType;

  @ViewChild('eventPanel') eventPanel: EventPanelComponent;

  constructor(private api: ApiService, private loader: PageLoaderService) {}

  ngOnInit(): void {
    this.savingType = eSaveChangeType.Single;

    this.loader.show();
    this.api.getAllCocktails().subscribe(response => {
      this.cocktails = response;
      this.dataLoaded = true;
      this.loader.hide();
    });
  }

  toggleSavingType() {
    if (this.savingType === eSaveChangeType.Single) {
      this.savingType = eSaveChangeType.Batch;
      this.saveBtnEnabled = true;
    } else {
      this.savingType = eSaveChangeType.Single;
      this.saveBtnEnabled = false;
    }
  }

  createCocktail() {
    let cocktail = {
      name: "Bees knees",
      strength: 30,
      ingredients: []
    } as ICocktailDto;

    this.api.createCocktail(cocktail).subscribe(result => {
      console.log(result);
      this.eventPanel.getEvents();
    });
  }
}
