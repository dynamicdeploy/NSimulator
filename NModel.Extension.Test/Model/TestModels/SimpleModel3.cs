namespace NModel.Extension.Test {
    public static class SimpleModel3 {
        private static IModel Model;

        public static IModel Make () {
            if (Model == null) {
                var model = new ExplicitModel ();

                model.AddState ("1", "a");
                model.AddState ("2");
                model.AddState ("3", "b");
                model.AddState ("4", "a");
                model.AddState ("5", "a");
                model.AddState ("6", "b");

                model.AddTransition (0, 1);
                model.AddTransition (1, 2);
                model.AddTransition (1, 3);
                model.AddTransition (2, 3);
                model.AddTransition (2, 4);
                model.AddTransition (3, 5);
                model.AddTransition (4, 4);
                model.AddTransition (4, 0);
                model.AddTransition (5, 5);
                model.AddTransition (5, 0);

                Model = model;
            }

            return Model;
        }
    }
}
