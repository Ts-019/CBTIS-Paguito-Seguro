using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DictaduraChatbot.Botones
{
    public class Menu
    {

        public static async Task<DialogTurnResult> ShowOptions(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var option = await stepContext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("¿Qué es lo que usted necesita?"),
                    Choices = ChoiceFactory.ToChoices(new List<string> { "Pago", "Preguntas Frecuentes", "Salir"}),
                    Style = ListStyle.HeroCard
                }, 
                cancellationToken);
            return option;
        }

    }
}
