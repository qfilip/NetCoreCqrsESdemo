import { Component, OnInit, ViewChild } from '@angular/core';
import { PageLoaderService } from 'src/app/services/page-loader.service';
import { ApiService } from 'src/app/services/api.service';
import { IIngredientDto, ICommandInfo } from 'src/app/_generated/interfaces';
import { Command } from 'src/app/_notgenerated/helpers';
import { CreateIngredientDialog } from './create-ingredient-dialog/create-ingredient-dialog';
import { eCommand } from 'src/app/_generated/enums';
import { CommandHandler } from 'src/app/functions/commandFunctions';
import { eControllerType } from 'src/app/_notgenerated/enums';
import { NgForm } from '@angular/forms';
import { ConfirmDialogService } from 'src/app/services/confirm.service';

@Component({
    selector: 'app-ingredients',
    templateUrl: './ingredients.component.html',
    styleUrls: ['./ingredients.component.scss']
})
export class IngredientsComponent implements OnInit {

    ingredients: IIngredientDto[] = [];
    handler: CommandHandler;

    @ViewChild('acid') createDialog: CreateIngredientDialog;

    constructor(
        private controller: ApiService,
        private pageLoader: PageLoaderService,
        private confirmDialog: ConfirmDialogService
    ) { }

    ngOnInit() {
        this.getIngredients();
        this.handler = new CommandHandler();
    }

    private getIngredients() {
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

    saveChanges() {
        const changes = this.handler.getChanges();

        if (changes.length === 0) {
            this.confirmDialog.open('No changes were made', false).subscribe();
            return;
        }

        this.confirmDialog.open('Pending changes are about to be saved. Proceed?')
            .subscribe(proceed => {
                if (!!proceed) {
                    let payloads: ICommandInfo<IIngredientDto>[] = [];

                    changes.forEach(x => {
                        const payload = {
                            type: x.commandType,
                            dto: x.parameter
                        } as ICommandInfo<IIngredientDto>;

                        payloads.push(payload);
                    });

                    this.controller.executeCommands(payloads, eControllerType.Ingredient)
                        .subscribe(result => {
                            this.cleanStackUpdateEntries(result);
                        });
                }
            });
    }

    undo() {
        this.handler.reverse();
    }

    createCommand(e: IIngredientDto) {
        for (let i = 0; i < 10; i++) {
            const description = `Created ${e.name} ingredient`;
            const command = new Command(e, this.ingredients, eCommand.CreateIngredientCommand, description);
            this.handler.execute(command);
        }
    }

    private cleanStackUpdateEntries(commands: ICommandInfo<IIngredientDto>[]) {
        this.handler.cleanStack();

        commands.forEach(x => {
            const command = new Command(x.dto, this.ingredients, x.type);
            this.handler.execute(command, false);
        });
    }
}
