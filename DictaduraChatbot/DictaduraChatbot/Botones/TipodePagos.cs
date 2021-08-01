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
    public class TipodePagos
    {

        public static async Task<DialogTurnResult> ShowOptions(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var option = await stepContext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Seleccione lo que gusta pagar: "),
                    Choices = ChoiceFactory.ToChoices(new List<string> { "Pago1", "Pago2", "Pago3" }),
                    Style = ListStyle.HeroCard
                },
                cancellationToken);
            return option;
        }

    }
}
