///*
//In this example two concrete builders inherit from an abstract base class
//By returning the builder instance from method calls we are following the "fluent api" design

//eg var foo = new Builder().AppendThing().AppendThing();

//However inheritance breaks fluent design as the wrong concrete type is returned when calling a method on the base class
//This can be fixed with generics (curiously recurring template pattern)

//credit: https://blog.mzikmund.com/2017/01/c-builder-pattern-with-inheritance/
//*/

//abstract class GameBuilder<TGame, TBuilder>
//    where TGame : Game
//    // the concrete class must inherit from GameBuilder<Game, Builder>
//    // but since the builder will inherit from GameBuilder this is true by definition
//    where TBuilder : GameBuilder<TGame, TBuilder> 
//{
//    private readonly TBuilder _builderInstance = null;
//    protected int _boardSize = 8;
//    protected int _level = 1;

//    public GameBuilder()
//    {
//        //store the concrete builder instance
//        //this is so that we can return it in builder methods 
//        _builderInstance = (TBuilder)this;
//    }

//    public TBuilder BoardSize(int boardSize)
//    {
//        _boardSize = boardSize;
//        return _builderInstance;
//    }

//    public TBuilder Level(int level)
//    {
//        _level = level;
//        return _builderInstance;
//    }

//    public abstract TGame Build();
//}

//class LocalGameBuilder : GameBuilder<LocalGame, LocalGameBuilder>
//{
//    private int _aiStrength = 3;

//    protected override LocalGameBuilder BuilderInstance => this;

//    public LocalGameBuilder AiStrength(int aiStrength)
//    {
//        _aiStrength = aiStrength;
//        return _builderInstance; // return this; would also work
//    }

//    //now only LocalGame can be built by LocalGameBuilder
//    public override LocalGame Build() =>
//        new LocalGame(_aiStrength, _boardSize, _level);
//}

//class OnlineGameBuilder : GameBuilder<OnlineGame, OnlineGameBuilder>
//{
//    private string _serverUrl = "http://example.com/";

//    protected override OnlineGameBuilder BuilderInstance => this;

//    public OnlineGameBuilder ServerUrl(string serverUrl)
//    {
//        _serverUrl = serverUrl;
//        return _builderInstance; // return this; would also work
//    }

//    public override OnlineGame Build() =>
//        new OnlineGame(_serverUrl, _boardSize, _level);
//}