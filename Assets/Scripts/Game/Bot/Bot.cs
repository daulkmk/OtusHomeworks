using UnityEngine;
using VContainer;

public class Bot : Unit
{
    [SerializeField] private float _targetReachedDistance = 1;
    
    public MoveToTargetComponent MoveToTargetComponent { get; private set; }
    public PatrolPointsComponent PatrolPointsComponent { get; private set; } = new();
    public BotBrain BotBrain { get; private set; }

    private BotBlackboardFactory _blackboardFactory;

    [Inject]
    public void Construct(BotBlackboardFactory blackboardFactory)
    {
        _blackboardFactory = blackboardFactory;
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();

        MoveToTargetComponent = new MoveToTargetComponent(moveComponent: MoveComponent, 
            lookDirectionComponent: LookDirectionComponent,
            targetReachedDistance: _targetReachedDistance
        );

        var blackboard = _blackboardFactory.Create(this);
        BotBrain = new BotBrain(blackboard);

        Add(MoveToTargetComponent);

        _tickables.Add(MoveToTargetComponent);
        _tickables.Add(BotBrain);

        _disposables.Add(BotBrain);

        BotBrain.Initialize();
    }
}