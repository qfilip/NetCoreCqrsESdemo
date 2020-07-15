import { Command } from '../_notgenerated/helpers';
import { IBaseDto, ICommandInfo } from '../_generated/interfaces';
import { eCommandType } from '../_generated/enums';

export class CommandHandler<TDto extends IBaseDto> {
    localChanges: { index: number, commandType: eCommandType, description: string }[];

    private stack: Command[];
    private index: number;
    
    constructor() {
        this.stack = [];
        this.index = -1;
    }

    execute(command: Command, trackChange: boolean = true) {
        command.execute();
        if (trackChange) {
            this.stack.push(command);
            this.index++;

            this.refreshLocalChanges();
        }
    }

    reverse() {
        if(this.index < 0)
            return;

        const command = this.stack[this.index];
        command.reverse();
        this.stack.pop();
        this.index--;

        this.refreshLocalChanges();
    }

    revertToIndex(index: number) {
        this.stack.forEach((x, i) => {
            if(i > index) {
                this.reverse();
            }
        });
    }

    getCommandPayload() {
        let commandsPayload = [];
        const mapPayload = (x: Command) => 
            { return  { command: x.command, commandType: x.commandType, dto: x.parameter} as ICommandInfo<TDto> };

        this.stack.forEach(x => commandsPayload.push(mapPayload(x)));


        return commandsPayload;
    }

    cleanStack() {
        this.stack = [];
        this.index = -1;

        this.refreshLocalChanges();
    }

    private refreshLocalChanges() {
        let updatedChanges = [];
        this.stack.forEach((x, i) => updatedChanges.push({ index: i, commandType: x.commandType, description: x.description }));
        
        this.localChanges = [...updatedChanges];
    }
}