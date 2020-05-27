var firstTimeLogin = document.getElementById('isFirstTimeLogin');
firstTimeLogin = firstTimeLogin == null ? false : firstTimeLogin.value;
if (firstTimeLogin == 'True') {
    console.log("Entering the  Tour");
        productTour();
    }


function productTour() {

    var steps = [];


    const tour = new Shepherd.Tour({

        defaults: {
            cancelIcon: true,
            classes: 'shepherd-theme-arrows'
        }
    });

    steps.push(['.profile bottom', 'Welcome', 'Please follow the steps to understand how the challenge works']);
    steps.push(['.printer left', 'Print Report', 'You can generate the report and save for future references']);
    steps.push(['.levelcol top', 'Levels', 'There are 5 levels in total. Only one level will be unlocked at a time. As you complete a level, the next level will be unlocked and you can proceed with the tasks. Once you complete all the levels, you will become a planet hero.']);
    steps.push(['.taskcol top', 'Tasks', 'There are 4 tasks in every level. Once you complete a task, mark it as completed by clicking the \'Mark as Completed\' button below. You will not be able to access the next task immediately. The timer on the top of the levels section will display the time left for next task to be available.']);
    steps.push(['.prog top', 'Progress', 'There are 4 cards which will update after the completion of every task and will show you the current level you are in, the number of pending and completed tasks for that level, and  the total score for all the tasks from all the levels completed till now']);
    //steps.push(['.analytics right', 'Web Analytics', 'Take the tour to learn more about how to use the Web Analytics reports']);

    steps.push(['.linc bottom', 'Activity', 'This chart will be updated after every task is completed and will help you to track your progress by showing the overall points scored over the days.']);
    steps.push(['.carbc bottom', 'Carbon Activity', 'This is a carbon chart which will help you to know how much carbon emission have you reduced by performing the tasks. It will show you the percentage of carbon reduced from energy, transport and household sectors.']);



    for (i = 0; i < steps.length; i++) {
        buttons = [];
        // no back button at the start

        buttons.push({
            text: 'Skip Tour',
            classes: 'shepherd.shepherd-theme-arrows.shepherd-has-title .shepherd-content header a.shepherd-cancel-link-button-secondary',
            action: function () {
                return tour.cancel();
            }
        });
        if (i > 0) {
            buttons.push({
                text: 'Back',
                classes: 'shepherd-button-secondary',
                action: function () {
                    return tour.back();
                }
            });
        }
        // no next button on last step
        if (i != (steps.length - 1)) {
            buttons.push({
                text: 'Next',
                classes: 'shepherd-button-primary',
                action: function () {
                    return tour.next();
                }
            });
        } else {
            buttons.push({
                text: 'Close',
                classes: 'shepherd-button-primary',
                action: function () {
                    return tour.hide();
                }
            });
        }

        tour.addStep('step_' + i, {
            text: steps[i][2],
            title: steps[i][1],
            attachTo: steps[i][0],
            //classes: 'shepherd shepherd-open shepherd-theme-arrows shepherd-transparent-text',

            buttons: buttons,
            cancelIcon: true
        });

    }
    tour.start();
}