import { Component, OnInit, ViewChild } from '@angular/core';
import { PageLoaderService } from 'src/app/services/page-loader.service';
import { ApiService } from 'src/app/services/api.service';
import { IIngredientDto, ICommandPayload } from 'src/app/_generated/interfaces';
import { Command } from 'src/app/_notgenerated/helpers';
import { CreateIngredientDialog } from './create-ingredient-dialog/create-ingredient-dialog';
import { eCommand } from 'src/app/_generated/enums';
import { CommandHandler } from 'src/app/functions/commandFunctions';
import { eControllerType } from 'src/app/_notgenerated/enums';

@Component({
    selector: 'app-ingredients',
    templateUrl: './ingredients.component.html',
    styleUrls: ['./ingredients.component.scss']
})
export class IngredientsComponent implements OnInit {

    ingredients: IIngredientDto[] = [];
    private handler: CommandHandler;

    @ViewChild('acid') createDialog: CreateIngredientDialog;

        constructor(
            private controller: ApiService,
            private pageLoader: PageLoaderService) { }

    ngOnInit() {
        this.getIngredients();
        this.handler = new CommandHandler();
    }

    private getIngredients() {
        this.pageLoader.show('Fetching ingredients');
        this.controller.getAllIngredients()
        .subscribe(
            response => {
                this.ingredients = response;
                this.pageLoader.hide();
            },
            error => {
                this.pageLoader.hide();
        });
    }

    openCreateDialog() {
        this.createDialog.open();
    }

    saveChanges() {
        const changes = this.handler.getChanges();
        let payloads: ICommandPayload<IIngredientDto>[] = [];
        changes.forEach(x => {
            const payload = {
                commandType: x.commandType,
                payload: x.parameter
            } as ICommandPayload<IIngredientDto>;
            payloads.push(payload);
        });

        this.controller.executeCommands(payloads, eControllerType.Ingredient)
            .subscribe(r => console.log(r));
    }

    undo() {
        this.handler.reverse();
    }

    createCommand(e: IIngredientDto) {
        const command = new Command(e, this.ingredients, eCommand.CreateIngredientCommand);
        this.handler.execute(command);
    }

}
