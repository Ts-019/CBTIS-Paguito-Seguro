using DictaduraChatbot.Botones;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DictaduraChatbot.Dialogos

{
    public class RootDialog : ComponentDialog
    {

        protected int ID;
        protected string Password;
        protected string Opcion;
        protected string Tipo;

        public RootDialog()
        {
            var dialogoCascada = new WaterfallStep[]
                  {
                     ShowMenu,
                     GoOption,
                     SubOption,
                     SetPassword,
                     ShowData,
                     End
                     };

            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), dialogoCascada));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new NumberPrompt<int>(nameof(NumberPrompt<int>), ValidateID));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
        }

        private async Task<DialogTurnResult> ShowData(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            Password = stepContext.Context.Activity.Text;
            await stepContext.Context.SendActivityAsync($"Muchas gracias alumno con el ID {ID}.");

            return await End(stepContext, cancellationToken);
        }

        private async Task<DialogTurnResult> SubOption(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var option = stepContext.Context.Activity.Text;
            await stepContext.Context.SendActivityAsync($"Seleccionaste {option}");
            if (Opcion.Equals("Pago"))
            {
                Tipo = option;
                return await SetID(stepContext, cancellationToken);
            }
            else
            {
                switch (option)
                {
                    case "Pregunta1":
                        await stepContext.Context.SendActivityAsync("Respuesta1");
                        break;
                    case "Pregunta2":
                        await stepContext.Context.SendActivityAsync("Respuesta2");
                        break;
                    case "Pregunta3":
                        await stepContext.Context.SendActivityAsync("Respuesta3");
                        break;
                    default:
                        break;
                }
            }
            return await End(stepContext, cancellationToken);
        }

        private async Task<DialogTurnResult> SetPassword(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            ID = Int32.Parse(stepContext.Context.Activity.Text);
            return await stepContext.PromptAsync(nameof(TextPrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Para comprobar su identidad, por favor, ingrese su contraseña."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> GoOption(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var option = stepContext.Context.Activity.Text;
            Opcion = option;
            switch (option)
            {
                case "Pago":
                    return await TipodePagos.ShowOptions(stepContext, cancellationToken);
                case "Preguntas Frecuentes":
                    return await Preguntas.ShowOptions(stepContext, cancellationToken);
                case "Salir":
                    return await End(stepContext, cancellationToken);
                default:
                    await stepContext.Context.SendActivityAsync("Seleccionaste una opción invalida.");
                    break;
            }
            return await End(stepContext,cancellationToken);
        }

        private async Task<DialogTurnResult> ShowMenu(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await Menu.ShowOptions(stepContext, cancellationToken);
        }

        private async Task<DialogTurnResult> SetID(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync(nameof(NumberPrompt<int>),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Para continuar con el pago, por favor, ingrese su ID."),
                    RetryPrompt = MessageFactory.Text("Por favor, ingrese un ID valido.")
                },
                cancellationToken);
        }

        private async Task<bool> ValidateID(PromptValidatorContext<int> promptContext, CancellationToken cancellationToken)
        {
            return await Task.FromResult(
                promptContext.Recognized.Succeeded &&
                promptContext.Recognized.Value > 0);
        }

        private async Task<DialogTurnResult> End(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync("Muchas gracias por utilizar la asistencia virtual. Para comenzar de nuevo, escriba cualquier cosa.");
            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }
    }
}
