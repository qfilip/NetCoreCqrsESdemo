import { Component, OnInit, ViewChild } from '@angular/core';
import { PageLoaderService } from 'src/app/services/page-loader.service';
import { ApiService } from 'src/app/services/api.service';
import { IIngredientDto, ICommandInfo, IAppEventDto } from 'src/app/_generated/interfaces';
import { Command } from 'src/app/_notgenerated/helpers';
import { CreateIngredientDialog } from './create-ingredient-dialog/create-ingredient.dialog';
import { eCommand, eCommandType } from 'src/app/_generated/enums';
import { CommandHandler } from 'src/app/functions/commandFunctions';
import { eControllerType } from 'src/app/_notgenerated/enums';
import { NgForm } from '@angular/forms';
import { ConfirmDialogService } from 'src/app/services/confirm.service';
import { EditIngredientDialog } from './edit-ingredient-dialog/edit-ingredient.dialog';

@Component({
    selector: 'app-ingredients',
    templateUrl: './ingredients.component.html',
    styleUrls: ['./ingredients.component.scss']
})
export class IngredientsComponent implements OnInit {

    ingredients: IIngredientDto[] = [];
    handler: CommandHandler<IIngredientDto>;

    @ViewChild('acid') createDialog: CreateIngredientDialog;
    @ViewChild('aeid') editDialog: EditIngredientDialog;

    constructor(
        private controller: ApiService,
        private pageLoader: PageLoaderService,
        private confirmDialog: ConfirmDialogService
    ) { }

    ngOnInit() {
        this.getIngredients();
        this.handler = new CommandHandler<IIngredientDto>();
    }

    private getIngredients() {
        this.ingredients = [];
        this.pageLoader.show('Fetching ingredients');
        this.controller.getAll<IIngredientDto>(eControllerType.Ingredient)
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

    openEditDialog(e: IIngredientDto) {
        this.editDialog.open(e);
    }

    saveChanges() {
        const payloads = this.handler.getCommandPayload();

        if (payloads.length === 0) {
            this.confirmDialog.open('No changes were made', false).subscribe();
            return;
        }

        this.confirmDialog.open('Pending changes are about to be saved. Proceed?')
            .subscribe(proceed => {
                if (!!proceed) {

                    this.controller.executeCommands<IIngredientDto>(payloads, eControllerType.Ingredient)
                        .subscribe(result => {
                            this.cleanStackUpdateEntries(result);
                            this.changesSavedSuccessMessage();
                        });
                }
            });
    }

    undo() {
        this.handler.reverse();
    }

    revertToChange(changeIndex: number) {
        this.handler.revertToChange(changeIndex);
    }

    onIngredientCreate(e: IIngredientDto) {
        const description = `Created ${e.name} ingredient`;
        const command = new Command(e, this.ingredients, eCommand.CreateIngredientCommand, eCommandType.Create, description);
        this.handler.execute(command);
    }

    onIngredientEdit(e: IIngredientDto) {
        const description = `Edited ${e.name} ingredient`;
        const command = new Command(e, this.ingredients, eCommand.EditIngredientCommand, eCommandType.Edit, description);
        this.handler.execute(command);
    }

    private cleanStackUpdateEntries(commands: ICommandInfo<IIngredientDto>[]) {
        this.getIngredients();
        this.handler.cleanStack();

        commands.forEach(x => {
            const command = new Command(x.dto, this.ingredients, x.command, x.commandType);
            this.handler.execute(command, false);
        });
    }

    private changesSavedSuccessMessage() {
        const msg = 'Local changes are saved in the database, and transfered to events page';
        const cancelVsible = false;
        this.confirmDialog.open(msg, cancelVsible).subscribe();
    }

    clogData() {
        console.table(this.ingredients);
    }

    reseed() {
        this.controller.reseedDb().subscribe(r => console.log(r));
    }
}
