import { eCommand, eCommandType } from '../_generated/enums';
import { IBaseDto } from '../_generated/interfaces';

export class PageLoaderInfo {
    loading: boolean;
    message: string;
}

export class ConfirmDialogInfo {
    visible: boolean;
    message: string;
    okLabel: string;
    cancelLabel: string;
    cancelVisible: boolean;
}

export class Command {
    private array: IBaseDto[];

    parameter: IBaseDto;
    command: eCommand;
    commandType: eCommandType;
    description: string;
    
    constructor(parameter: IBaseDto, array: IBaseDto[], command: eCommand, commandType: eCommandType, description: string = 'no description') {
        this.parameter = parameter;
        this.array = array;
        this.command = command;
        this.commandType = commandType;
        this.description = description;
    }
    
    execute() {
        if (this.commandType === eCommandType.Create) {
            this.array.push(this.parameter);
            return;
        }
        if (this.commandType === eCommandType.Edit) {
            const index = this.array.findIndex(x => x.id === this.parameter.id);
            this.array[index] = this.parameter;
            return;
        }
        if (this.commandType === eCommandType.Delete) {
            const index = this.array.findIndex(x => x.id === this.parameter.id);
            this.array.splice(index, 1);
            return;
        }
    }

    reverse() {
        const index = this.array.findIndex(x => x.id === this.parameter.id);
        this.array.splice(index, 1);
    }
}